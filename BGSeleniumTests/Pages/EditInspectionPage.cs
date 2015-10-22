using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BGSeleniumTests.Pages
{
    public class EditInspectionPage : PageBase
    {
        public EditInspectionPage(IWebDriver driver) : base(driver) {}

        public ViewInspectionPage ChangeStatus(string status)
        {
            var statusList = _driver.FindElementWait(By.XPath("(//div[@class='requiredInput']//select)[2]"));
            var statusField = new SelectElement(statusList);
            statusField.SelectByText(status);

            var saveButton = _driver.FindElementWait(By.XPath("//td[@id='topButtonRow']//input[@title='Save']"));
            saveButton.Click();
            var page = new ViewInspectionPage(_driver);
            return page;
        }

    }
}