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

            methods.LoginPage("336"); // INPUT REQUIRED PAGE 333 || 335|| 336||337 
            methods.SetUpPageFilters();
            methods.SetUpChoosenPeriod("2015 W03"); // INPUT PERIOD IN FORMAT   2016 03_MAR || 2016 W18 || 2016 Q1
            methods.StorePageData();
            methods.LoginPage277();
            methods.SetUpChoosenPeriod277("2015 W03"); // INPUT PERIOD IN FORMAT   2016 03_MAR || 2016 W18 || 2016 Q1
            methods.CheckData();
            methods.email_send("Check data Chainsfs Common Test &" + "..." + ". Period: " + methods.CheckingPeriod); //INPUT FILE NAME

            firefox.Quit();
        }
    }
}
