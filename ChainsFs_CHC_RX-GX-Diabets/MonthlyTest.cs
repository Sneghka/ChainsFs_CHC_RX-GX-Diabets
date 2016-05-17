using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using ChainsFs_CHC_RX_GX_Diabets.WebPageElements;




namespace ChainsFs_CHC_RX_GX_Diabets
{
    [TestFixture]
    public class MonthlyTest
    {

        [Test]
        public void CheckDataMonth_CHC()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("333");
            methods.SetUpPageFilters();
            methods.SetUpMonthPeriod();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForMonthPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & CHC. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }

        [Test]
        public void CheckDataMonth_RX()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("335");
            methods.SetUpPageFilters();
            methods.SetUpMonthPeriod();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForMonthPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & RX. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }

        [Test]
        public void CheckDataMonth_GX()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("336");
            methods.SetUpPageFilters();
            methods.SetUpMonthPeriod();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForMonthPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & GX. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }
        [Test]
        public void CheckDataMonth_Diabetes()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("337");
            methods.SetUpPageFilters();
            methods.SetUpMonthPeriod();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForMonthPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & Diabetes. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }
    




    }
}
