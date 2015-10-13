using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BGSeleniumTests
{
    public class LicensesPage : PageBase
    {
        public LicensesPage(IWebDriver driver) : base(driver){}

        public string PageTitle => _driver.FindElementText(By.XPath("//td[@id='bodyCell']//h1"));

        protected override string GetPageUrl()
        {
            throw new NotImplementedException();
        }


    }
}
