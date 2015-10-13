using System;
using System.Linq;
using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class LicensesPage : PageBase
    {
        public LicensesPage(IWebDriver driver) : base(driver){}

        public string PageTitle => _driver.FindElementText(By.XPath("//td[@id='bodyCell']//h1"));

        protected override string GetPageUrl()
        {
            throw new NotImplementedException();
        }

        public string CurrentView => _driver.FindElementText(By.
            XPath("//select[@id='fcf']/option[@selected='selected']"));

        public NewLicensePage OpenNewLicensePage()
        {
            var newButton = _driver.FindElementWait(By.XPath("//input[@value=' New ']"));
            newButton.Click();
            var newLicensePage = new NewLicensePage(_driver);
            return newLicensePage;
        }
    }
}
