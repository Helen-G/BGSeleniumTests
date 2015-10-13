using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGSeleniumTests.Pages;
using OpenQA.Selenium;

namespace BGSeleniumTests
{
    public class TabBar
    {
        protected readonly IWebDriver _driver;

        public TabBar(IWebDriver driver)
        {
            _driver = driver;
        }

        public LicensesPage OpenLicensesPage()
        {
            OpenTab("Licenses Tab");
            return new LicensesPage(_driver);
        }

        private IWebElement OpenTab(string tabName)
        {
            var name = $"//a[@title=\'{tabName}\']";
            var element = _driver.FindElementWait(By.XPath(name));
            element.Click();
            return element;
        }
    }
}
