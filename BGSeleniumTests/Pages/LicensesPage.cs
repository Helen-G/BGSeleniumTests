using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    class LicensesPage : PageBase
    {
        public LicensesPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetAppMenuItem
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
    }
}
