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
    public class Methods
    {
        private readonly FirefoxDriver _firefox;
        private List<string> marketList = new List<string>();
        private List<string> brandList = new List<string>();
        private List<string> valueList = new List<string>();
        private List<string> valueList277 = new List<string>();
        private List<string> messageContent = new List<string>();

        public string CheckigPeriod { get; set; }

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
                    Thread.Sleep(4000);
                    break;
                }
                Thread.Sleep(4000);
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
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(60));
            _firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            var pageElements = new PageElements(_firefox);
            _firefox.Navigate().GoToUrl("http://pharmxplorer.com.ua/" + pageNumber);

            var loginElement = pageElements.LoginElement;
            var passwordElement = pageElements.PasswordElement;
            var loginButton = pageElements.LoginButton;
            loginElement.SendKeys("full_test");
            passwordElement.SendKeys("aspirin222");
            loginButton.Click();
            WaitForAjax();
            Thread.Sleep(6000);
            var continueButton = pageElements.ContinueButton;
            continueButton.Click();
            WaitForAjax();
        }


        public void SetUpPageFilters()
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(20));
            WaitForAjax();
            var pageElements = new PageElements(_firefox);
            WaitForAjax();
            var salesRadioButton = pageElements.SalesRadioButton;
            salesRadioButton.Click();
            WaitForAjax();
            WaitForAjax();
            var globalMarket = pageElements.CheckCurrencyMarket.Text;
            var currency = globalMarket.Substring(globalMarket.Length - 4);
            Debug.WriteLine("Текущая валюта: " + currency);
            var currencyToggle = pageElements.CurrencyToggle;

            if (currency == "kUAH")
            {
                currencyToggle.Click();
                WaitForAjax();
                var newCurrency = pageElements.CheckCurrencyMarket;
                wait.Until(ExpectedConditions.TextToBePresentInElement(newCurrency, " UAH"));
                Debug.WriteLine("Текущая валюта: " + newCurrency.Text);
            }
        }

        public void StorePageData() //store data from CHC/RG / GX /Diabets
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(20));
            var pageElements = new PageElements(_firefox);
            CheckigPeriod = pageElements.ChoosenPeriodText.GetAttribute("title");
            var removeMarket = " Market";
            var removeSanofi = " Sanofi";
            const int MAGIC_NUMBER = 8;

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
                if (i == 5 && markets[i] == "ENTEROGERMINA Market + FS") //ENTEROGERMINA Market + FS for CHC
                {
                    marketList.Add(markets[i]);
                    Debug.WriteLine("Market - " + marketList[i - 1]);
                    continue;
                }
                marketList.Add(markets[i].Substring(0, markets[i].Length - removeMarket.Length));

                Debug.WriteLine("Market - " + marketList[i - 1]);
            }


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                var lenght = brands[i].Length;
                brandList.Add(brands[i].Substring(4, lenght - removeSanofi.Length - 4));
                Debug.WriteLine("Brand - " + brandList[i - 1]);
            }


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                valueList.Add(values[i]);
                Debug.WriteLine("Value - " + valueList[i - 1]);
            }
        }


        public void LoginPage277()
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(20));
            //_firefox.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(60));
            var pageElements = new PageElements(_firefox);
            _firefox.Navigate().GoToUrl("http://pharmxplorer.com.ua/277");
            WaitForAjax();

            pageElements.SelectMarketSearchElement277.Click();
            WaitForAjax();
            pageElements.SelectMarketSearchInputField277.SendKeys("Sanofi");
            WaitForAjax();
            pageElements.SelectedMarketElement277.Click();
            WaitForAjax();
            Thread.Sleep(10000);
            wait.Until(ExpectedConditions.ElementToBeClickable(pageElements.BeginButton277));
            pageElements.BeginButton277.Click();
            WaitForAjax();
            pageElements.TestTotalTab277.Click();
            WaitForAjax();
            Debug.WriteLine("Login success");
        }

        public void SetUpFilterForWeekPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodWeekButton277.Click();
            WaitForAjax();
        }
        public void SetUpFilterForMonthPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodMonthButton277.Click();
            WaitForAjax();
        }
        public void SetUpFilterForQrtPage277()
        {
            var pageElements = new PageElements(_firefox);
            pageElements.ChosePeriodQrtButton277.Click();
            WaitForAjax();
        }

        public void CheckData()
        {
            for (int i = 0; i < marketList.Count; i++)
            {
                var pageElements = new PageElements(_firefox);
                WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(20));

                /******SELECT BRAND******/
                pageElements.FilterButton277.Click();
                WaitForAjax();

                pageElements.BrandOption277.Click();
                WaitForAjax();
                pageElements.ClearBrandElement277.Click();
                WaitForAjax();
                pageElements.SearchBrandButton277.Click();
                WaitForAjax();

                pageElements.InputBrandField277.SendKeys(brandList[i]);
                WaitForAjax();

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
                WaitForAjax();
                Thread.Sleep(2000);


                /******SELECT GROUP******/
                pageElements.GroupButton277.Click();
                WaitForAjax();
                pageElements.SearchGroupButton277.Click();
                WaitForAjax();
                pageElements.InputGroupField277.SendKeys(marketList[i]);
                WaitForAjax();

                var selectedGroup = pageElements.SelectedGroupElement277;
                action.MoveToElement(selectedGroup, 10, 10).Click().Perform();
                WaitForAjax();
                Thread.Sleep(2000);

                /******COMPARE***********/
                var value277 = pageElements.TotalSum277.GetAttribute("title");
                valueList277.Add(value277);

                if (value277 == valueList[i])
                {
                    Debug.WriteLine("<html><font color=green>" + "<b>" + marketList[i] + "</b>" +" " + valueList[i] + " = " + value277 +
                                    ";</html>");
                    messageContent.Add("<html><font color=green>" + "<b>" + marketList[i] + "</b>" + " " + valueList[i] + " = " + value277 + ";</html>");
                }
                else
                {
                    Debug.WriteLine("<html><font color=red>" + "<b>" + marketList[i] + "</b>" + " " + valueList[i] + "  !! НЕ РАВНО !!  " +
                                    value277 + ";</html>");
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
            // mail.To.Add("nataly.tenkova@proximaresearch.com");
            mail.Subject = subject;
            mail.Body = MessageContent(messageContent);
            smtpServer.Send(mail);
        }
    }
}

