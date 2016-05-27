using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
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

        public void WaitForTextInTitleAttribute(string locator, string text)
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
                if (_firefox.FindElement(By.XPath(locator)).GetAttribute("title") == text)
                {
                    if (!first) Debug.WriteLine("Text is found: " + text);
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for text is present: " + text);

                first = false;
                Thread.Sleep(waitRetryDelayMs);
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
                if (_firefox.FindElement(By.CssSelector(locator)).GetAttribute("class") == text)
                {
                    if (!first) Debug.WriteLine("Text is found: '" + text);
                    break; //если элемент найден
                }

                if (first) Debug.WriteLine("Waiting for text is present: '" + text);

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
                Debug.WriteLine("Loop - element is visible");
                if (_firefox.FindElement(By.XPath(locator)).Text.ContainsIgnoreCase(text))
                {
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
            var pageElements = new PageElements(_firefox);
            _firefox.Navigate().GoToUrl("http://pharmxplorer.com.ua/" + pageNumber);
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("submit")));
            pageElements.LoginElement.SendKeys("full_test");
            pageElements.PasswordElement.SendKeys("aspirin222");
            pageElements.LoginButton.Click();
            // Continue через раз не кликается. К чему привязаться???
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ContinueButtonXPath)));
            pageElements.ContinueButton.Click();
        }

        public void SetUpPageFilters()
        {
            Debug.WriteLine("Login success");
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));
            var pageElements = new PageElements(_firefox);

            //через раз не видит кнопку Salase в DOM. Новый метод - ждёт и кликает! (тоже не срабатывает). 
            // Добавила в метод неявное ожидание загрузки страницы и неявное ожидание загрузки скриптов
            WaitForAjax();
            WaitAndClickElement(PageElements.SalesRadioButtonXPath);
            //
            Debug.WriteLine("Market after clicking sale button - " + pageElements.CheckCurrencyMarket.Text);
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
            WaitForAjax();
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodMonthButton.Click();
            WaitForAjax();

        }
        public void SetUpQuarterPeriod()
        {
            WaitForAjax();
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodQrtButton277.Click();
            WaitForAjax();

        }
        public void SetUpChoosenPeriod(string period)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

            var pageElements = new PageElements(_firefox);
            if (period.Contains("W"))
            {
                pageElements.ChosePeriodWeekButton.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ChoosenPeriodXpath)));
                Debug.WriteLine("choosen period week");
            }
            else if (period.Contains("Q"))
            {
                pageElements.ChosePeriodQrtButton.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ChoosenPeriodXpath)));
                Debug.WriteLine("choosen period QRT");
            }
            else
            {
                pageElements.ChosePeriodMonthButton.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ChoosenPeriodXpath)));
                Debug.WriteLine("choosen period month");
            }

            var action = new Actions(_firefox);
            pageElements.DropDownMenu.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.DropDownChoosenPeriodXPath)));
            action.ContextClick(pageElements.DropDownChoosenPeriod).Perform();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.SearchOptionContextMenuXPath)));
            pageElements.SearchOptionContextMenu.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputFieldChoosenPeriodXPath)));
            pageElements.InputFieldChoosenPeriod.SendKeys(period);
            Debug.WriteLine("Wait text in title" + period);
            WaitForTextInTitleAttribute(PageElements.DropDownChoosenPeriodXPath, period);
            Debug.WriteLine("Click choosen period");
            action.MoveToElement(pageElements.DropDownChoosenPeriod, 5, 5).Click().Perform();
            Debug.WriteLine("Wait text in title of choosen period" + period);
            WaitForAjax();
            WaitForTextInTitleAttribute(PageElements.ChoosenPeriodXpath, period);
            Debug.WriteLine("Choosen period " + pageElements.ChoosenPeriod.GetAttribute("title"));
        }

        public void StorePageData() //store data from CHC/RG / GX /Diabets
        {
            Debug.WriteLine("SetUpPageFilters method success");
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
                    //Debug.WriteLine("Market - " + marketList[i - 1]);
                    continue;
                }
                marketList.Add(markets[i].Substring(0, markets[i].Length - removeMarket.Length));

                //Debug.WriteLine("Market - " + marketList[i - 1]);
            }


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                var lenght = brands[i].Length;
                brandList.Add(brands[i].Substring(4, lenght - removeSanofi.Length - 4));
                //Debug.WriteLine("Brand - " + brandList[i - 1]);
            }


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                valueList.Add(values[i]);
                //Debug.WriteLine("Value - " + valueList[i - 1]);
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
            _firefox.Navigate().GoToUrl("http://pharmxplorer.com.ua/277");
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.SelectMarketSearchElement277XPath)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.SelectMarketSearchElement277XPath)));
            pageElements.SelectMarketSearchElement277.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".PopupSearch>input")));
            pageElements.SelectMarketSearchInputField277.SendKeys("Sanofi");
            WaitForTextInTitleAttribute(PageElements.SelectedMarketElement277XPath, "Sanofi (France)");
            pageElements.SelectedMarketElement277.Click();
            Thread.Sleep(6000);
            Debug.WriteLine("Selected Market " + pageElements.SelectedMarketElement277.GetAttribute("title"));
            wait.Until(ExpectedConditions.ElementToBeClickable(pageElements.BeginButton277));
            pageElements.BeginButton277.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.TestTotalTabXPath)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.TestTotalTabXPath)));
            pageElements.TestTotalTab277.Click();
            Debug.WriteLine("Looking for filter button 277");
            WaitForAjax();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.FilterButton277Xpath)));
            Debug.WriteLine("Login 277 success");
        }

        public void SetUpFilterForWeekPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodWeekButton277.Click();
            WaitForAjax();
            Thread.Sleep(4000);
            Debug.WriteLine("Choosen period - week");
        }
        public void SetUpFilterForMonthPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodMonthButton277.Click();
            WaitForAjax();
            Debug.WriteLine("Choosen period - month");
        }
        public void SetUpFilterForQrtPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodQrtButton277.Click();
            WaitForAjax();
            Debug.WriteLine("Choosen period - Qrt");
        }
        public void SetUpChoosenPeriod277(string period)
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(".//*[@id='MainContainer']")));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.ChosePeriodWeekButton277XPath)));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.ChosePeriodWeekButton277XPath)));

            var pageElements = new PageElements(_firefox);
            if (period.Contains("W"))
            {
                pageElements.ChosePeriodWeekButton277.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.PeriodButton277XPath)));
                Debug.WriteLine("choosen period week");
            }
            else if (period.Contains("Q"))
            {
                pageElements.ChosePeriodQrtButton277.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.PeriodButton277XPath)));
                Debug.WriteLine("choosen period QRT");
            }
            else
            {
                pageElements.ChosePeriodMonthButton277.Click();
                WaitForAjax();
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.PeriodButton277XPath)));
                Debug.WriteLine("choosen period month");
            }

            var action = new Actions(_firefox);
            pageElements.PeriodButton277.Click();
            /*
                        wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath(".//*[@id='MainContainer']")));
                        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.DropDownMenu277XPath)));*/

            //Не находит дроп даун меню после выбора периода. Пробую функцию waitAndClkick
            WaitAndClickElement(PageElements.DropDownMenu277XPath);
            Debug.WriteLine("WaitAndClick works!");

            /*pageElements.DropDownPeriodMenu277.Click();*/
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.DropDownChoosenPeriodXPath)));
            Debug.WriteLine("DropDownChoosenPeriod is visible");
            action.ContextClick(pageElements.DropDownChoosenPeriod).Perform();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("html/body/ul/li[1]/a/span")));
            pageElements.SearchOptionContextMenu.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("html/body/div[2]/input")));
            pageElements.InputFieldChoosenPeriod.SendKeys(period);
            WaitForAjax();
            action.MoveToElement(pageElements.DropDownChoosenPeriod, 5, 5).Click().Perform();
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(PageElements.FilterButton277Xpath)));
        }

        public void CheckData()
        {
            for (int i = 0; i < marketList.Count; i++)
            {
                var pageElements = new PageElements(_firefox);
                WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(120));

                /******SELECT BRAND******/
                pageElements.FilterButton277.Click();
                Debug.WriteLine("Click Filter button");

                WaitAndClickElement(PageElements.BrandOption277Xpath);
                Debug.WriteLine("Click Brand option");

                WaitAndClickElement(PageElements.ClearBrandElement277XPath);
                Debug.WriteLine("Clear brand");

                WaitAndClickElement(PageElements.SearchBrandButton277XPath);
                Debug.WriteLine("Click search brand button");

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputBrandOrGroupField277XPath)));
                pageElements.InputBrandField277.SendKeys(brandList[i]);

                Thread.Sleep(6000);

                var action = new Actions(_firefox);
                var selectedBrand277 = pageElements.SelectedBrand277;
                var selectedBrandName = pageElements.SelectedBrandName277.GetAttribute("title");
                if (selectedBrandName != brandList[i])
                {
                    action.MoveToElement(_firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div[2]/div[1]")), 10, 10).Click().Perform();
                }
                else
                {
                    action.MoveToElement(selectedBrand277, 10, 10).Click().Perform();
                }


                /******SELECT GROUP******/
                WaitAndClickElement(PageElements.GroupButton277XPath);
                Debug.WriteLine("Click Group button");

                WaitAndClickElement(PageElements.ClearGroupElement277XPath);
                Debug.WriteLine("Click clear button");

                WaitAndClickElement(PageElements.SearchGroupButton277XPath);
                Debug.WriteLine("Click search group button");

                wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(PageElements.InputBrandOrGroupField277XPath)));
                pageElements.InputGroupField277.SendKeys(marketList[i]);
                Debug.WriteLine("Input field - send group name: " + marketList[i]);

                Debug.WriteLine("Title selected group before waiting - " + pageElements.SelectedGroupElement277.GetAttribute("title"));
                //WaitForTextInTitleAttribute(PageElements.SelectedGroupElement277XPath, marketList[i]);
                // !!!! ВИСНЕТ !!!! НА ДРУГИХ ТЕКСТАХ МЕТОД ОТРАБАТЫВАЕТ!!!
                //Debug.WriteLine("Check ContainsIgnoreCase! ");

                Thread.Sleep(8000);

               var selectedGroup = pageElements.SelectedGroupElement277;
                action.MoveToElement(selectedGroup, 10, 10).Click().Perform();
                Debug.WriteLine("Click selected group");

                WaitForTextInClassAttribute("//*[@class='QvFrame Document_LB2694']", "QvSelected");
                Debug.WriteLine("Check waitFortextInClassAttribute!!! ");

                WaitForAjax();
                Thread.Sleep(8000);
                
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
            //mail.To.Add("nataly.tenkova@proximaresearch.com");
            mail.Subject = subject;
            mail.Body = MessageContent(messageContent);
            smtpServer.Send(mail);
        }
    }
}

