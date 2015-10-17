using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BGSeleniumTests.Pages
{
    public class NewLicensePage : PageBase
    {
        public NewLicensePage(IWebDriver driver) : base(driver)
        {
        }

        public ViewLicensePage CreateLicense(string type)
        {
            //select a type
            var typeList = _driver.FindElement(By.Id("00NF000000BHoFp"));
            var typeField = new SelectElement(typeList);
            typeField.SelectByText(type);
            var applicantLookup = _driver.FindElementWait(By.Id("CF00NF000000BHoFV_lkwgt"));
            
            string currentWindow = _driver.CurrentWindowHandle;
            var finder = new PopupWindowFinder(_driver);

            //click the lookup icon, switch to the window and iframe
            var lookupWindowHandle = finder.Click(applicantLookup);
            _driver.SwitchTo().Window(lookupWindowHandle);
            _driver.SwitchTo().Frame("resultsFrame");

            var applicantName = _driver.FindElementWait(By.XPath("//a[text()='test']"));
            applicantName.Click();
            _driver.SwitchTo().Window(currentWindow);

            //click Save button
            var saveButton = _driver.FindElementWait(By.XPath("//input[@value=' Save ']"));
            saveButton.Click();
            var viewLicensePage = new ViewLicensePage(_driver);
            return viewLicensePage;
        }


        protected override string GetPageUrl()
        {
            throw new NotImplementedException();
        }
    }
}