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
    public class WeeklyTest
    {
        [Test]
        public void CheckDataWeek_CHC()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("333");
            methods.SetUpPageFilters();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForWeekPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & CHC. Period: " + methods.CheckingPeriod);

             firefox.Quit();
        }

        [Test]
        public void CheckDataWeek_RX()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("335");
            methods.SetUpPageFilters();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForWeekPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & RX. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }

        [Test]
        public void CheckDataWeek_GX()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("336");
            methods.SetUpPageFilters();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForWeekPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & GX. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }
        [Test]
        public void CheckDataWeekDiabetes()
        {
            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);

            methods.LoginPage("337");
            methods.SetUpPageFilters();
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpFilterForWeekPage277();
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test & Diabetes. Period: " + methods.CheckingPeriod);

            firefox.Quit();
        }
    
    }
}
