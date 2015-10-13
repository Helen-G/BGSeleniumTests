using System;
using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class ViewLicensePage : PageBase
    {
        public ViewLicensePage(IWebDriver driver) : base(driver) { }

        public string Name
        {
            get { return _driver.FindElementText(By.XPath("//div[@class='content']/h2")); }
        }

        protected override string GetPageUrl()
        {
            throw new NotImplementedException();
        }
    }
}