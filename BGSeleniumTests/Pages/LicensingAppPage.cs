using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    class LicensingAppPage : PageBase
    {
        public LicensingAppPage(IWebDriver driver) : base(driver)
        {
        }

        public string AppMenuItem
        {
            get
            {
                return _driver.FindElementWait(By.Id("tsidLabel")).Text;
            }
        }

        protected override string GetPageUrl()
        {
            return "a02/o";
        }

        public List<string> DisplayedTabs()
        {
            var tabBar = _driver.FindElements(By.XPath("//ul[@id='tabBar']/li/a"));
            List<string> result = new List<string>();
            foreach (IWebElement tab in tabBar)
            {
                result.Add(tab.Text);
            }
            return result;
        }
    }
}
