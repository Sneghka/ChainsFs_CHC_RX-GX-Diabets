using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace ChainsFs_CHC_RX_GX_Diabets.WebPageElements
{
    //Elements for CHC/RX/GX/Diabets pages

    public class PageElements
    {
        private readonly FirefoxDriver _firefox;

        public PageElements(FirefoxDriver firefox)
        {
            _firefox = firefox;

        }

        //ELEMENTS FOR LOGIN
        public IWebElement LoginElement
        {
            get { return _firefox.FindElement(By.Id("username")); }
        }
        public IWebElement PasswordElement
        {
            get { return _firefox.FindElement(By.Id("password")); }
        }

        public IWebElement LoginButton
        {
            get { return _firefox.FindElement(By.Id("submit")); }
        }

        public IWebElement ContinueButton
        {
            get { return _firefox.FindElement(By.XPath("//div[@class='QvFrame Document_BU04']/div[2]/button")); }
        }

        //PAGE ELEMENTS
        /******PERIOD******/
        
        public IWebElement ChoosenPeriod {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_MB04']/div[2]/div/div[1]/div[5]/div/div[3]/div[1]")); }
        }
        public IWebElement ChoosenPeriodText
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_MB04']/div[2]/div/div[1]/div[5]/div/div[3]")); }
        }
        public IWebElement ChosePeriodWeekButton
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB18']/div[3]/div/div[1]/div[1]/div[1]")); }
        }
        public IWebElement ChosePeriodMonthButton
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB18']/div[3]/div/div[1]/div[2]/div[1]")); }
        }
        public IWebElement ChosePeriodQrtButton
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB18']/div[3]/div/div[1]/div[3]/div[1]")); }
        }

        public IWebElement DropDownMenu
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_MB04']/div[2]/div/div[1]/div[5]/div/div[1]/div[1]")); }
        }

        public IWebElement DropDownPeriodMenu
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame DS']/div/div/div[1]")); }
        }

        public IWebElement InputFieldChoosenPeriod
        {
            get { return _firefox.FindElement(By.XPath("html/body/div[2]/input")); }
        }

        public IWebElement DropDownChoosenPeriod
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame DS']/div/div/div[1]/div[1]")); }
        }

        public IWebElement SearchOptionContextMenu
        {
            get { return _firefox.FindElement(By.XPath("html/body/ul/li[1]/a/span")); }
        }


        /*********Filters*********/
        public IWebElement SalesRadioButton
        {
            get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB129']/div[3]/div/div[1]/div[1]/div[1]")); }
        }

        public IWebElement CurrencyToggle
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_BU56']/div[3]/button")); }
        }

        public IWebElement CheckCurrencyMarket
        {
            get { return _firefox.FindElement(By.XPath("//div[@class='QvFrame Document_CH19']/div[2]/div[2]/div")); }
        }

        /**MARKET NAME**/
        private string[] _marketXPaths =
        {
            "",
            ".//*[@class='QvFrame Document_CH19']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH24']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH110']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH25']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH23']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH26']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH104']/div[3]/div[1]/div[1]/div[1]/div/div[1]",
            ".//*[@class='QvFrame Document_CH112']/div[3]/div[1]/div[1]/div[1]/div/div[1]"
        };

        public IWebElement GetMarket(int n)
        {
            return _firefox.FindElement(By.XPath(_marketXPaths[n]));
        }
      

        /**BRAND NAME**/

        private string[] _brandXPaths =
        {
            "",
            ".//*[@class='QvFrame Document_CH19']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH24']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH110']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH25']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH23']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH26']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH104']/div[3]/div[1]/div[1]/div[2]/div/div[3]",
            ".//*[@class='QvFrame Document_CH112']/div[3]/div[1]/div[1]/div[2]/div/div[3]"
        };

        public IWebElement GetBrand(int n)
        {
            return _firefox.FindElement(By.XPath(_brandXPaths[n]));
        }
        

        /***VALUE**/

        private string[] _valueXPaths =
        {
            "",
            ".//*[@class='QvFrame Document_CH19']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH24']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH110']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH25']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH23']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH26']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH104']/div[3]/div[1]/div[1]/div[5]/div/div[6]",
            ".//*[@class='QvFrame Document_CH112']/div[3]/div[1]/div[1]/div[5]/div/div[6]"
        };

        public IWebElement GetValue(int n)
        {
            return _firefox.FindElement(By.XPath(_valueXPaths[n]));
        }

        
        /********Page Elements (277)*********/

        /***LOGIN PAGE277***/

        public IWebElement SelectMarketSearchElement277
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB1207']/div[2]/div[1]/div")); }
        }

        public IWebElement SelectMarketSearchInputField277
        {
            get { return _firefox.FindElement(By.CssSelector(".PopupSearch>input")); }
        }

        public IWebElement SelectedMarketElement277
        {
            get{ return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB1207']/div[3]/div/div[1]/div/div[1]"));}
        }
         public IWebElement BeginButton277
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_BU2056']/div[2]/button")); }
        }

        /*****SET UP FILTERS******/
         public IWebElement TestTotalTab277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@rel='DocumentSH163']/a")); }
         }
         public IWebElement ChosePeriodWeekButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB2626']/div[3]/div/div[1]/div[1]/div[1]")); }
         }
         public IWebElement ChosePeriodMonthButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB2626']/div[3]/div/div[1]/div[2]/div[1]")); }
         }
         public IWebElement ChosePeriodQrtButton277
         {
             get { return _firefox.FindElements(By.XPath(".//*[@class='QvFrame Document_LB2626']/div[3]/div/div[1]/div[3]/div[1]"))[1]; }
         }
         public IWebElement PeriodButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_TX3506']")); }
              /*get { return _firefox.FindElements(By.XPath(".//*[@class='QvFrame Document_TX3506']/div[2]/table/tbody/tr/td"))[1]; }*/
         }

        public IWebElement DropDownPeriodMenu277
        {
            get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_MB585']/div[3]/div/div[1]/div[5]/div/div[3]")); }
        }

        /***CHOSE BRAND****/
         public IWebElement FilterButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_TX3803']/div[2]/table/tbody/tr/td")); }
         }
         public IWebElement BrandOption277
         {
             get { return _firefox.FindElement(By.XPath("//div[@class='QvFrame Document_LB2667']/div[3]/div/div[1]/div[1]")); }
         }
         public IWebElement ClearBrandElement277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[2]/div[1]/div[2]")); }
         }
         public IWebElement SearchBrandButton277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[2]/div[1]/div[3]")); }
                                                       //div[@class='QvFrame Document_LB2694']/div[2]/div/div[3]
         }
         public IWebElement InputBrandField277
         {
             get { return _firefox.FindElement(By.XPath("html/body/div[2]/input")); }
         }

         public IWebElement SelectedBrand277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div[1]/div[1]")); }
         }
         public IWebElement SelectedBrandName277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2694']/div[3]/div/div[1]/div[1]")); }
         }

        /***********CHOSE GROUP*************/
        public IWebElement GroupButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_TX3495']/div[2]/table/tbody/tr/td")); }
         }
         public IWebElement ClearGroupElement277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB2372']/div[2]/div[1]/div[1]")); }
         }
         public IWebElement SearchGroupButton277
         {
             get { return _firefox.FindElement(By.XPath(".//*[@class='QvFrame Document_LB2372']/div[2]/div[1]/div[2]")); }
         }
         public IWebElement InputGroupField277
         {
             get { return _firefox.FindElement(By.XPath("html/body/div[2]/input")); }
         }
         public IWebElement SelectedGroupElement277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_LB2372']/div[3]/div/div[1]/div[1]")); }
         }
         public IWebElement TotalSum277
         {
             get { return _firefox.FindElement(By.XPath("//*[@class='QvFrame Document_CH1694']/div[3]/div[1]/div[1]/div[5]/div/div[1]")); }
         }



        /*******CHOSE GROUP*****/
        //driver.findElement(By.xpath("//div[@class='QvFrame Document_LB2372']/div[3]/div/div[1]/div[1]/div[2]/div[2]/span[1]")) - первый элемент в выпадающем списке групп
    }
}
