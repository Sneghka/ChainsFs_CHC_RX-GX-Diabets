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
            loginElement.SendKeys("");                              //ENTER LOGIN
            passwordElement.SendKeys("");                           //ENTER PASSWORD
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
            var salesRadioButton = pageElements.SalesRadioButton;
            salesRadioButton.Click();
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
            }


            //var market1 = pageElements.Market1.GetAttribute("title");
            //var market2 = pageElements.Market2.GetAttribute("title");
            //var market3 = pageElements.Market3.GetAttribute("title");
            //var market4 = pageElements.Market4.GetAttribute("title");
            //var market5 = pageElements.Market5.GetAttribute("title"); //ENTEROGERMINA Market + FS for CHC
            //var market6 = pageElements.Market6.GetAttribute("title");
            //var market7 = pageElements.Market7.GetAttribute("title");
            //var market8 = pageElements.Market8.GetAttribute("title");

            var brand1 = pageElements.Brand1.GetAttribute("title");
            var brand2 = pageElements.Brand2.GetAttribute("title");
            var brand3 = pageElements.Brand3.GetAttribute("title");
            var brand4 = pageElements.Brand4.GetAttribute("title");
            var brand5 = pageElements.Brand5.GetAttribute("title");
            var brand6 = pageElements.Brand6.GetAttribute("title");
            var brand7 = pageElements.Brand7.GetAttribute("title");
            var brand8 = pageElements.Brand8.GetAttribute("title");

            var value1 = pageElements.Value1.GetAttribute("title");
            var value2 = pageElements.Value2.GetAttribute("title");
            var value3 = pageElements.Value3.GetAttribute("title");
            var value4 = pageElements.Value4.GetAttribute("title");
            var value5 = pageElements.Value5.GetAttribute("title");
            var value6 = pageElements.Value6.GetAttribute("title");
            var value7 = pageElements.Value7.GetAttribute("title");
            var value8 = pageElements.Value8.GetAttribute("title");


            for (int i = 1; i <= MAGIC_NUMBER; i++)
            {
                if (i == 5 && markets[i] == "ENTEROGERMINA Market + FS") //ENTEROGERMINA Market + FS for CHC
                {
                    marketList.Add(markets[i]);
                    continue;
                }
                marketList.Add(markets[i].Substring(0, markets[i].Length - removeMarket.Length));
            }

            //marketList.Add(market1.Substring(0, market1.Length - removeMarket.Length));
            //marketList.Add(market2.Substring(0, market2.Length - removeMarket.Length));
            //marketList.Add(market3.Substring(0, market3.Length - removeMarket.Length));
            //marketList.Add(market4.Substring(0, market4.Length - removeMarket.Length));

            //if (market5 != "ENTEROGERMINA Market + FS") //ENTEROGERMINA Market + FS for CHC
            //{
            //    marketList.Add(market5.Substring(0, market5.Length - removeMarket.Length));
            //}
            //else
            //{
            //    marketList.Add(market5);
            //}

            //marketList.Add(market6.Substring(0, market6.Length - removeMarket.Length));
            //marketList.Add(market7.Substring(0, market7.Length - removeMarket.Length));
            //marketList.Add(market8.Substring(0, market8.Length - removeMarket.Length));


            brandList.Add(brand1.Substring(4, brand1.Length - removeSanofi.Length - 4));
            brandList.Add(brand2.Substring(4, brand2.Length - removeSanofi.Length - 4));
            brandList.Add(brand3.Substring(4, brand3.Length - removeSanofi.Length - 4));
            brandList.Add(brand4.Substring(4, brand4.Length - removeSanofi.Length - 4));
            brandList.Add(brand5.Substring(4, brand5.Length - removeSanofi.Length - 4));
            brandList.Add(brand6.Substring(4, brand6.Length - removeSanofi.Length - 4));
            brandList.Add(brand7.Substring(4, brand7.Length - removeSanofi.Length - 4));
            brandList.Add(brand8.Substring(4, brand8.Length - removeSanofi.Length - 4));

            valueList.Add(value1);
            valueList.Add(value2);
            valueList.Add(value3);
            valueList.Add(value4);
            valueList.Add(value5);
            valueList.Add(value6);
            valueList.Add(value7);
            valueList.Add(value8);

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
            //wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(".//*[@rel='DocumentSH163']/a")));
            pageElements.TestTotalTab277.Click();
            WaitForAjax();
            Thread.Sleep(6000);
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
                /* pageElements.ClearBrandElement277.Click();
                  WaitForAjax();*/
                pageElements.SearchBrandButton277.Click();
                WaitForAjax();

                Debug.WriteLine("Search " + brandList[i] + " brand");
                //pageElements.InputBrandField277.SendKeys(brandList[i] + Keys.Enter);
                pageElements.InputBrandField277.SendKeys(brandList[i]);
                WaitForAjax();
                Debug.WriteLine("Highlighted brand is - " + _firefox.FindElement(By.XPath(".//*[@id='164']/div[3]/div/div[1]/div/div[2]")).GetAttribute("title"));
                Debug.WriteLine("Text in span is - " + _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div/div[2]/div[2]/span")).Text);

                var action = new Actions(_firefox);
                var selectedBrand277 = pageElements.SelectedBrand277;
                var element = _firefox.FindElement(By.XPath("//div[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div/div[2]/div[2]/span"));
                action.MoveToElement(selectedBrand277, 10, 10).Click().Perform();
               /* selectedBrand277.Click();*/

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

