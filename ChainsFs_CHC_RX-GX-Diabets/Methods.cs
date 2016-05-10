using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var continueButton = pageElements.ContinueButton;
            continueButton.Click();
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
                //wait.Until(ExpectedConditions.TextToBePresentInElement(pageElements.Market1, "ESSENTIALE Market, UAH"));
                //WaitForAjax();
                var newCurrency = pageElements.CheckCurrencyMarket.Text;
                Debug.WriteLine("Текущая валюта: " + newCurrency);
            }
        }

        public void StorePageData() //store data from CHC/RG / GX /Diabets
        {
            WebDriverWait wait = new WebDriverWait(_firefox, TimeSpan.FromSeconds(20));
            var pageElements = new PageElements(_firefox);
            var removeMarket = " Market";
            var removeSanofi = " Sanofi";
            const int MAGIC_NUMBER = 8;

            var markets = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                markets[i] = pageElements.GetMarket(i).GetAttribute("title");
                Debug.WriteLine(markets[i]);
            }

            var brands = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
               
                brands[i] = pageElements.GetBrand(i).GetAttribute("title");
                Debug.WriteLine(brands[i]);
            }
          
            var values = new string[MAGIC_NUMBER + 1];
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
               
                values[i] = pageElements.GetValue(i).GetAttribute("title");
                Debug.WriteLine(values[i]);
            }
           
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                if (i == 5 && markets[i] == "ENTEROGERMINA Market + FS") //ENTEROGERMINA Market + FS for CHC
                {
                    marketList.Add(markets[i]);
                    continue;
                }
                marketList.Add(markets[i].Substring(0, markets[i].Length - removeMarket.Length));
               
                Debug.WriteLine(marketList[i-1]);
            }

          
            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
               
                var lenght = brands[i].Length;
                brandList.Add(brands[i].Substring(4, lenght - removeSanofi.Length-4));
                Debug.WriteLine(brandList[i-1] + "проверка цикла получения названия брендов");
            }
            Debug.WriteLine("Конец цикла добавления brand в список");

            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                valueList.Add(brands[i-1]);
            }


            Debug.WriteLine("market - " + marketList[0] + "/" + "brand - " + brandList[0] + "/" + "value - " + valueList[0]);
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

                //wait.Until(ExpectedConditions.ElementExists(By.XPath("pageElements.BrandOption277")));
                pageElements.BrandOption277.Click();
                WaitForAjax();
                pageElements.ClearBrandElement277.Click();
                WaitForAjax();
                pageElements.SearchBrandButton277.Click();
                WaitForAjax();

                Debug.WriteLine("Search " + brandList[i] + " brand");

                pageElements.InputBrandField277.SendKeys(brandList[i]);
                WaitForAjax();

                Debug.WriteLine("Text in span is - " + _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div/div[2]/div[2]/span")).Text);

                var action = new Actions(_firefox);
                var selectedBrand277 = pageElements.SelectedBrand277;
                action.MoveToElement(selectedBrand277, 10, 10).Click().Perform();


                WaitForAjax();
                //Debug.WriteLine("Highlighted element " + pageElements.SelectedBrand277.Text);
                Debug.WriteLine("Brand was selected " + _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div")).GetAttribute("title") + " - total " + pageElements.TotalSum277.GetAttribute("title"));

                WaitForAjax();



                /******SELECT GROUP******/
                /*  pageElements.GroupButton277.Click();
                  WaitForAjax();
                  pageElements.SearchGroupButton277.Click();
                  WaitForAjax();
                  pageElements.InputGroupField277.SendKeys(marketList[i]);
                  WaitForAjax();
                  pageElements.SelectedGroupElement277.Click();
                  WaitForAjax();*/

                /******COMPARE***********/
                /*var value277 = pageElements.TotalSum277.GetAttribute("title");
                valueList277.Add(value277);

                if (value277 == valueList[i])
                    Debug.WriteLine("<html><font color=green>" + marketList[i] + " " + valueList[i] + " = " + value277 + ";</html>");
                else
                {
                    Debug.WriteLine("<html><font color=red>" + marketList[i] + " " + valueList[i] + " НЕ РАВНО " + value277 + ";</html>");
                }*/

            }

        }




    }
}

