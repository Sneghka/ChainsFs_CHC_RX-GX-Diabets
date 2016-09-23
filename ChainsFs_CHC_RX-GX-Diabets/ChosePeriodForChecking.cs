using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace ChainsFs_CHC_RX_GX_Diabets
{
    [TestFixture]
    public class ChosePeriodForChecking
    {
        [Test]
        public void ChoseAnyPeriodAndAnyFileForChecking()
        {

            var firefox = new FirefoxDriver();
            var methods = new Methods(firefox);
            var data = "2016 W37";
            var pageNumber = "337";     //335_RX, 336_GX, 337_Diabets, 333_CHC
            var pageName = "Diabets";

            methods.LoginPage(pageNumber); // INPUT REQUIRED PAGE 333 || 335|| 336||337 
            methods.SetUpPageFilters();
            methods.SetUpChoosenPeriod(data); // INPUT PERIOD IN FORMAT   2016 05_May || 2016 W18 || 2016 Q1
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpChoosenPeriod277(data); // INPUT PERIOD IN FORMAT   2016 05_May || 2016 W18 || 2016 Q1
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test &" + pageName + ". Period: " + methods.CheckingPeriod); //INPUT FILE NAME

            firefox.Quit();
        }
    }
}
