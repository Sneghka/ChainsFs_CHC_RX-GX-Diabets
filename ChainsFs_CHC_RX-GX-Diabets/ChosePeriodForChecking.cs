using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChainsFs_CHC_RX_GX_Diabets.WebPageElements;
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
            var data = "2017 W08";//INPUT PERIOD IN FORMAT   2016 05_May || 2016 W18 || 2016 Q1

            var dictionary = new Dictionary<string, string>();
            dictionary.Add("333", "CHC");
            dictionary.Add("335", "RX");
            dictionary.Add("336", "GX");
            dictionary.Add("337", "Diabets");

           
            foreach (var d in dictionary)
            {
                var firefox = new FirefoxDriver();
                var methods = new Methods(firefox);

                methods.LoginPage(d.Key); // INPUT REQUIRED PAGE 333 || 335|| 336||337 
                methods.SetUpPageFilters();
                methods.SetUpChoosenPeriod(data); // INPUT PERIOD IN FORMAT   2016 05_May || 2016 W18 || 2016 Q1
                methods.StorePageData();
                methods.LoginPage277();
                methods.SetUpChoosenPeriod277(data); // INPUT PERIOD IN FORMAT   2016 05_May || 2016 W18 || 2016 Q1
                methods.CheckData();
                methods.email_send("Check data Chainsfs Common Test &" + d.Value + ". Period: " + methods.CheckingPeriod); //INPUT FILE NAME
                firefox.Quit();
            }

            //firefox.Quit();

        }
    }
}
