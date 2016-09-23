using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using ChainsFs_CHC_RX_GX_Diabets.WebPageElements;
using NUnit.Framework;
using OpenQA.Selenium.Interactions;




namespace ChainsFs_CHC_RX_GX_Diabets
{

    public static class StringExtensions
    {
        public static bool ContainsIgnoreCase(this string source, string toCheck)
        {
            return source.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }

    public class Methods
    {
        private readonly FirefoxDriver _firefox;
        private List<string> marketList = new List<string>();
        private List<string> brandList = new List<string>();
        private List<string> valueList = new List<string>();
        private List<string> valueList277 = new List<string>();
        private List<string> messageContent = new List<string>();

        public string CheckingPeriod { get; set; }

        // private List<List<string>> dataList = new List<List<string>>(); 


        public Methods(FirefoxDriver firefox)
        {
            _firefox = firefox;
        }


        public void WaitForAjax()
        {
            while (true) // Handle timeout somewhere
            {
                var ajaxIsComplete = (bool)(_firefox as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0");
                if (ajaxIsComplete)
                {
                    Thread.Sleep(3000);
                    break;
                }
                Thread.Sleep(3000);
            }
        }

        public void TryToLoadPage(string url, string waitPresenceAllElementsByXPath)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            for (int i = 1; i < 6; i++)
            {
                try
                {
                    _firefox.Navigate().GoToUrl(url);
                    wait.Until(
                        ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(waitPresenceAllElementsByXPath)));
                    return;
                }
                catch (Exception)
                {
                   Console.WriteLine("Load page 277. Attempt №" + i);
                    i++;
                }

            }

        }

        public void TryToClickWithoutException(string locator, IWebElement element)
        {
            //Console.WriteLine("In tryToClick method + xpath: " + locator);
            var maxElementRetries = 100;
            var action = new Actions(_firefox);
            var retries = 0;
            while (true)
            {
                /*WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));*/

                try
                {
                    WebDriverWait wait = new WebDriverWait(new SystemClock(), _firefox, TimeSpan.FromSeconds(120),
                        TimeSpan.FromSeconds(5));
                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                    _firefox.FindElement(By.XPath(locator)).Click();
                    WaitForAjax();
                    return;
                }
                catch (Exception e)
                {
                    if (retries < maxElementRetries)
                    {
                        retries++;
                        Debug.WriteLine(retries);
                    }
                    else
                    {
                        throw e;
                    }
                }
               
            }


        }

        public void WaitForTextInTitleAttribute(string locator, string text)
        {
            /*WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));*/
            const int waitRetryDelayMs = 1000; //шаг итерации (задержка)
            const int timeOut = 500; //время тайм маута 
            bool first = true;

            for (int milliSecond = 0; ; milliSecond += waitRetryDelayMs)
            {
                WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

                try
                {
                    if (milliSecond > timeOut * 10000)
                    {
                        Debug.WriteLine("Timeout: Text " + text + " is not found ");
                        break; //если время ожидания закончилось (элемент за выделенное время не был найден)
                    }

                    wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                    if (_firefox.FindElement(By.XPath(locator)).GetAttribute("title") == text)
                    {
                        Thread.Sleep(2000);
                        if (!first) Debug.WriteLine("Text is found: " + text);
                        break; //если элемент найден
                    }

                    if (first) Debug.WriteLine("Waiting for text is present: " + text);

                    first = false;
                    Thread.Sleep(waitRetryDelayMs);
                }
                catch (StaleElementReferenceException a)
                {
                    if (milliSecond < timeOut * 10000)
                        continue;
                    else
                    {
                        throw a;
                    }
                }
                catch (NoSuchElementException b)
                {
                    if (milliSecond < timeOut * 10000)
                        continue;
                    else
                    {
                        throw b;
                    }
                }
            }

        }

        public void WaitForTextInClassAttribute(string locator, string text)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            const int waitRetryDelayMs = 1000; //шаг итерации (задержка)
            const int timeOut = 500; //время тайм маута 
            bool first = true;

            for (int milliSecond = 0; ; milliSecond += waitRetryDelayMs)
            {
                if (milliSecond > timeOut * 10000)
                {
                    Debug.WriteLine("Timeout: Text '" + text + "' is not found ");
                    break; //если время ожидания закончилось (элемент за выделенное время не был найден)
                }
                _firefox.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
                _firefox.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                if (_firefox.FindElement(By.XPath(locator)).GetAttribute("class") == text)
                {
                    Thread.Sleep(2000);
                    if (!first) Debug.WriteLine("Text is found: " + text);
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for text is present: " + text);

                first = false;
                Thread.Sleep(waitRetryDelayMs);
            }
        }


        public void WaitForTextInClassAttributeTwoPossiblePatterns(string locator, string text1, string text2)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            const int waitRetryDelayMs = 1000; //шаг итерации (задержка)
            const int timeOut = 500; //время тайм маута 
            bool first = true;

            for (int milliSecond = 0; ; milliSecond += waitRetryDelayMs)
            {
                if (milliSecond > timeOut * 10000)
                {
                    Debug.WriteLine("Timeout: Text " + text1 +"/ "+text2+ " is not found ");
                    break; //если время ожидания закончилось (элемент за выделенное время не был найден)
                }
                _firefox.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
                _firefox.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                if (_firefox.FindElement(By.XPath(locator)).GetAttribute("class") == text1 || _firefox.FindElement(By.XPath(locator)).GetAttribute("class") == text2)
                {
                    Thread.Sleep(2000);
                    if (!first) Debug.WriteLine("Text is found: " + text1+"/"+text2);
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for text is present: " + text1+"/" + text2);

                first = false;
                Thread.Sleep(waitRetryDelayMs);
            }
        }


        public void WaitPatternPresentInText(string locator, string text)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            const int waitRetryDelayMs = 1000; //шаг итерации (задержка)
            const int timeOut = 500; //время тайм маута 
            bool first = true;

            for (int milliSecond = 0; ; milliSecond += waitRetryDelayMs)
            {
                if (milliSecond > timeOut * 10000)
                {
                    Debug.WriteLine("Timeout: Text " + text + " is not found ");
                    break; //если время ожидания закончилось (элемент за выделенное время не был найден)
                }
                _firefox.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
                _firefox.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                if (_firefox.FindElement(By.XPath(locator)).Text.ContainsIgnoreCase(text))
                {
                    Thread.Sleep(2000);
                    if (!first) Debug.WriteLine("Text is found: " + text);
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for text is present: " + text);

                first = false;
                Thread.Sleep(waitRetryDelayMs);
            }
        }

        public void WaitAndClickElement(string locator)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            const int waitRetryDelayMs = 1000; //шаг итерации (задержка)
            const int timeOut = 500; //время тайм маута 
            bool first = true;

            for (int milliSecond = 0; ; milliSecond += waitRetryDelayMs)
            {
                if (milliSecond > timeOut * 10000)
                {
                    Debug.WriteLine("Timeout: Element is not found by locator - " + locator);
                    break; //если время ожидания закончилось (элемент за выделенное время не был найден)
                }
                _firefox.Manage().Timeouts().SetPageLoadTimeout(TimeSpan.FromSeconds(60));
                _firefox.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(60));
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(locator)));
                Thread.Sleep(2000);
                if (_firefox.FindElement(By.XPath(locator)).Enabled)
                {
                    _firefox.FindElement(By.XPath(locator)).Click();
                    WaitForAjax();
                    if (!first) Debug.WriteLine("Element is disable");
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for is clickable");

                first = false;
                Thread.Sleep(waitRetryDelayMs);
            }
        }

        public string MessageContent(List<string> list)
        {
            var sb = new StringBuilder();
            foreach (var str in list)
            {
                sb.AppendLine(str);
                sb.AppendLine("<br>");

            }
            return sb.ToString();
        }

        public void LoginPage(string pageNumber)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            var action = new Actions(_firefox);
            var pageElements = new PageElements(_firefox);
            _firefox.Navigate().GoToUrl("http://pharmxplorer.com.ua/" + pageNumber);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("submit")));
            pageElements.LoginElement.SendKeys("full_test");
            pageElements.PasswordElement.SendKeys("aspirin222");
            action.MoveToElement(pageElements.LoginButton, 10, 10).Click().Perform();
            Thread.Sleep(4000);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ContinueButtonXPath)));
            TryToClickWithoutException(PageElements.ContinueButtonXPath, pageElements.ContinueButton);
        }

        public void SetUpPageFilters()
        {

            Debug.WriteLine("Login success");

            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            var pageElements = new PageElements(_firefox);

            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.SalesRadioButtonXPath)));
            TryToClickWithoutException(PageElements.SalesRadioButtonXPath, pageElements.SalesRadioButton);
            WaitPatternPresentInText(PageElements.CheckCurrencyMarketXPath, ",");
            var globalMarket = pageElements.CheckCurrencyMarket.Text;
            var currency = globalMarket.Substring(globalMarket.Length - 4);
            Debug.WriteLine("Текущая валюта: " + currency);
            var currencyToggle = pageElements.CurrencyToggle;

            if (currency == "kUAH")
            {
                currencyToggle.Click();
                WaitPatternPresentInText(PageElements.CheckCurrencyMarketXPath, " UAH");
                Debug.WriteLine("Новая текущая валюта: " + pageElements.CheckCurrencyMarket.Text);
            }
        }

        public void SetUpMonthPeriod()
        {
            Debug.WriteLine("Set up filters -  success");
            WaitForAjax();
            var pageElements = new PageElements(_firefox);
            TryToClickWithoutException(PageElements.ChosePeriodMonthkButtonXPath, pageElements.ChosePeriodMonthButton);
            WaitForAjax();
            WaitForTextInClassAttribute(PageElements.ChosePeriodMonthkButtonXPath, "QvSelected");

        }
        public void SetUpQuarterPeriod()
        {
            Debug.WriteLine("Set up filters -  success");
            var pageElements = new PageElements(_firefox);
            WaitForAjax();
            pageElements.ChosePeriodQrtButton.Click();
            WaitForTextInClassAttribute(PageElements.ChosePeriodQrtkButtonXPath, "QvSelected");
            WaitForAjax();

        }
        public void SetUpChoosenPeriod(string period)
        {
            Debug.WriteLine("Set up filters -  success");
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

            var pageElements = new PageElements(_firefox);
            if (period.Contains("W"))
            {
                TryToClickWithoutException(PageElements.ChosePeriodWeekButtonXPath, pageElements.ChosePeriodWeekButton);
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodWeekButtonXPath, "QvSelected");
                Debug.WriteLine("choosen period week");
            }
            else if (period.Contains("Q"))
            {
                pageElements.ChosePeriodQrtButton.Click();
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodQrtkButtonXPath, "QvSelected");
                Debug.WriteLine("choosen period QRT");
            }
            else
            {
                pageElements.ChosePeriodMonthButton.Click();
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodMonthkButtonXPath, "QvSelected");
                Debug.WriteLine("choosen period month");
            }

            var action = new Actions(_firefox);
            TryToClickWithoutException(PageElements.DropDownMenuXPath, pageElements.DropDownMenu);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.DropDownChoosenPeriodXPath)));
            action.ContextClick(pageElements.DropDownChoosenPeriod).Perform();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.SearchOptionContextMenuXPath)));
            pageElements.SearchOptionContextMenu.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputFieldChoosenPeriodXPath)));
            pageElements.InputFieldChoosenPeriod.SendKeys(period);
            WaitForTextInTitleAttribute(PageElements.DropDownChoosenPeriodXPath, period);
            action.MoveToElement(pageElements.DropDownChoosenPeriod, 5, 5).Click().Perform();
            WaitForAjax();
            WaitForTextInTitleAttribute(PageElements.ChoosenPeriodXpath, period);
            Debug.WriteLine("Choosen period " + pageElements.ChoosenPeriod.GetAttribute("title"));
        }

        public void StorePageData() //store data from CHC/RG / GX /Diabets
        {
            Debug.WriteLine("SetUpPeriod method success");
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            var pageElements = new PageElements(_firefox);
            CheckingPeriod = pageElements.ChoosenPeriodText.GetAttribute("title");
            var removeMarket = " Market";
            var removeSanofi = " Sanofi";
            var currentUrl = _firefox.Url;
            int MAGIC_NUMBER;
            if (currentUrl == "http://pharmxplorer.com.ua/337")
            {
                MAGIC_NUMBER = 2;
            }
            else
            {
                MAGIC_NUMBER = 8;
            }

            var markets = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                markets[i] = pageElements.GetMarket(i).GetAttribute("title");
            }

            var brands = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                brands[i] = pageElements.GetBrand(i).GetAttribute("title");
            }

            var values = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                values[i] = pageElements.GetValue(i).GetAttribute("title");
            }

            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                if (markets[i] == "ENTEROGERMINA Market + FS" || markets[i] == "PLAVIX Market" || markets[i] == "OADs Amaryl M Market" || markets[i] == "OADs Amaryl Market") //ENTEROGERMINA Market + FS for CHC
                {
                    marketList.Add(markets[i]);
                    continue;
                }
                marketList.Add(markets[i].Substring(0, markets[i].Length - removeMarket.Length));
            }

            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                var lenght = brands[i].Length;
                brandList.Add(brands[i].Substring(4, lenght - removeSanofi.Length - 4));
            }


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                valueList.Add(values[i]);
            }

            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                Debug.WriteLine(marketList[i - 1] + " (brand - " + brandList[i - 1] + ")  " + valueList[i - 1]);
            }
        }

        public void LoginPage277()
        {
            Debug.WriteLine("StorePageData method success");
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            var pageElements = new PageElements(_firefox);

            TryToLoadPage("http://pharmxplorer.com.ua/277", ".//*[@id='MainContainer']");

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.SelectMarketSearchElement277XPath)));
            TryToClickWithoutException(PageElements.SelectMarketSearchElement277XPath, pageElements.SelectMarketSearchElement277);
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".PopupSearch>input")));
            pageElements.SelectMarketSearchInputField277.SendKeys("Sanofi");
            WaitForTextInTitleAttribute(PageElements.SelectedMarketElement277XPath, "Sanofi (France)");
            TryToClickWithoutException(PageElements.SelectedMarketElement277XPath, pageElements.SelectedMarketElement277);
            WaitForTextInClassAttribute(PageElements.SelectedMarketElement277XPath, "QvSelected");
            Debug.WriteLine("Selected Market " + pageElements.SelectedMarketElement277.GetAttribute("title"));
            TryToClickWithoutException(PageElements.BeginButtonXPath277, pageElements.BeginButton277);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.TestTotalTabXPath)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.TestTotalTabXPath)));
            pageElements.TestTotalTab277.Click();
            WaitForAjax();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.FilterButton277Xpath)));
            Debug.WriteLine("Login 277 success");
        }

        public void SetUpFilterForWeekPage277()
        {
            var pageElements = new PageElements(_firefox);
            TryToClickWithoutException(PageElements.ChosePeriodWeekButton277XPath, pageElements.ChosePeriodWeekButton277);
            WaitForAjax();
            WaitForTextInClassAttribute(PageElements.ChosePeriodWeekButton277XPath, "QvSelected");
            Debug.WriteLine("choosen period week");
        }
        public void SetUpFilterForMonthPage277()
        {
            var pageElements = new PageElements(_firefox);
            TryToClickWithoutException(PageElements.ChosePeriodMonthButton277XPath, pageElements.ChosePeriodMonthButton277);
            WaitForAjax();
            WaitForTextInClassAttribute(PageElements.ChosePeriodMonthButton277XPath, "QvSelected");
            Debug.WriteLine("choosen period month");
        }
        public void SetUpFilterForQrtPage277()
        {
            var pageElements = new PageElements(_firefox);
            TryToClickWithoutException(PageElements.ChosePeriodQRTButton277XPath, pageElements.ChosePeriodQrtButton277);
            WaitForAjax();
            WaitForTextInClassAttribute(PageElements.ChosePeriodQRTButton277XPath, "QvSelected");
            Debug.WriteLine("choosen period QRT");
        }
        public void SetUpChoosenPeriod277(string period)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

            var pageElements = new PageElements(_firefox);
            if (period.Contains("W"))
            {
                TryToClickWithoutException(PageElements.ChosePeriodWeekButton277XPath, pageElements.ChosePeriodWeekButton277);
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodWeekButton277XPath, "QvSelected");
                Debug.WriteLine("choosen period week");
            }
            else if (period.Contains("Q"))
            {
                TryToClickWithoutException(PageElements.ChosePeriodQRTButton277XPath, pageElements.ChosePeriodQrtButton277);
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodQRTButton277XPath, "QvSelected");
                Debug.WriteLine("choosen period QRT");
            }
            else
            {
                TryToClickWithoutException(PageElements.ChosePeriodMonthButton277XPath, pageElements.ChosePeriodMonthButton277);
                WaitForAjax();
                WaitForTextInClassAttribute(PageElements.ChosePeriodMonthButton277XPath, "QvSelected");
                Debug.WriteLine("choosen period month");
            }

            var action = new Actions(_firefox);
            TryToClickWithoutException(PageElements.PeriodButton277XPath, pageElements.PeriodButton277);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.DropDownMenu277XPath)));
            TryToClickWithoutException(PageElements.DropDownMenu277XPath, pageElements.DropDownPeriodMenu277);
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.DropDownChoosenPeriodXPath)));
            action.ContextClick(pageElements.DropDownChoosenPeriod).Perform();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.SearchOptionContextMenuXPath)));
            pageElements.SearchOptionContextMenu.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("html/body/div[2]/input")));
            pageElements.InputFieldChoosenPeriod.SendKeys(period);
            WaitForAjax();
            action.MoveToElement(pageElements.DropDownChoosenPeriod, 5, 5).Click().Perform();
            WaitForTextInTitleAttribute(PageElements.DropDownPeriodMenu277XPath, period);
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.FilterButton277Xpath)));
        }

        public void CheckData()
        {
            Console.WriteLine("SetUp period 277 success");
            for (int i = 0; i < marketList.Count; i++)
            {
                var pageElements = new PageElements(_firefox);
                WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

                /******SELECT BRAND******/
                TryToClickWithoutException(PageElements.FilterButton277Xpath, pageElements.FilterButton277);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.BrandOption277Xpath)));
                TryToClickWithoutException(PageElements.BrandOption277Xpath, pageElements.BrandOption277);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.ClearBrandElement277XPath)));
                TryToClickWithoutException(PageElements.ClearBrandElement277XPath, pageElements.ClearBrandElement277);
                WaitForAjax();
                WaitForTextInClassAttribute(".//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div[1]", "QvOptional");
                TryToClickWithoutException(PageElements.SearchBrandButton277XPath, pageElements.SearchBrandButton277);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputBrandOrGroupField277XPath)));
                pageElements.InputBrandField277.SendKeys(brandList[i]);
                WaitForAjax();
                Thread.Sleep(500);
                var action = new Actions(_firefox);
                var selectedBrandName = pageElements.SelectedBrandName277.GetAttribute("title");
                if (selectedBrandName != brandList[i])
                {
                    action.MoveToElement(_firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div[2]/div[1]")), 10, 10).Click().Perform();
                }
                else
                {
                    action.MoveToElement(pageElements.SelectedBrand277, 10, 10).Click().Perform();
                }
                WaitForAjax();


                /******SELECT GROUP******/
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.GroupButton277XPath)));
                TryToClickWithoutException(PageElements.GroupButton277XPath, pageElements.GroupButton277);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.ClearGroupElement277XPath)));
                TryToClickWithoutException(PageElements.ClearGroupElement277XPath, pageElements.ClearGroupElement277);
                WaitForTextInClassAttribute(PageElements.SelectedGroupElement277XPath, "QvOptional");
                TryToClickWithoutException(PageElements.SearchGroupButton277XPath, pageElements.SearchGroupButton277);
                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputBrandOrGroupField277XPath)));
                pageElements.InputGroupField277.SendKeys(marketList[i]);
                WaitForAjax();
                Thread.Sleep(3000); // возможно лучше поставить ожидание 

                //*************************

                TryToClickWithoutException(PageElements.SelectedGroupElement277XPath, pageElements.SelectedGroupElement277);
                WaitForAjax();
                WaitForTextInClassAttributeTwoPossiblePatterns(PageElements.SelectedGroupElement277XPath, "QvSelected", "QvExcluded");
                Thread.Sleep(4000);
               
                /******COMPARE***********/
                var value277 = pageElements.TotalSum277.GetAttribute("title");
                valueList277.Add(value277);

                if (value277 == valueList[i])
                {
                    Debug.WriteLine(marketList[i] + ":  " + valueList[i] + " = " + value277 +
                                    ";");
                    messageContent.Add("<html><font color=green>" + "<b>" + marketList[i] + "</b>" + " " + valueList[i] + " = " + value277 + ";</html>");
                }
                else
                {
                    Debug.WriteLine(marketList[i] + ": " + " " + valueList[i] + "  !! НЕ РАВНО !!  " +
                                    value277 + ";");
                    messageContent.Add("<html><font color=red>" + marketList[i] + " " + valueList[i] + "  !! НЕ РАВНО !!  " +
                                    value277 + ";</html>");
                }

            }

        }

        public void email_send(string subject)
        {
            var mail = new MailMessage();
            mail.IsBodyHtml = true;
            var smtpServer = new SmtpClient("post.morion.ua");
            mail.From = new MailAddress("snizhana.nomirovska@proximaresearch.com");
            mail.To.Add("snizhana.nomirovska@proximaresearch.com");
            mail.To.Add("nataly.tenkova@proximaresearch.com");
            mail.Subject = subject;
            mail.Body = MessageContent(messageContent);
            smtpServer.Send(mail);
        }
    }
}

