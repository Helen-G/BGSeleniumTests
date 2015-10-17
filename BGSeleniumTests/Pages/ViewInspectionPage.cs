using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BGSeleniumTests.Pages
{
    public class ViewInspectionPage : PageBase
    {
        public ViewInspectionPage(IWebDriver driver) : base(driver) {}
        public string Status => _driver.FindElementText(By.XPath("//td[text()='Status']/following-sibling::td/div"));

        public ViewCheckListPage OpenChecklistPage()
        {
            var checklistButton = _driver.FindElementWait(By.XPath("//input[@value='Checklist']"));
            checklistButton.Click();
            var page = new ViewCheckListPage(_driver);
            return page;
        }

        public EditInspectionPage OpenEditInspectionPage()
        {
            var editButton = _driver.FindElementWait(By.XPath("//td[@id='topButtonRow']" +
                                                              "//input[@value=' Edit ']"));
            editButton.Click();
            var page = new EditInspectionPage(_driver);
            return page;
        }
    }

    public class EditInspectionPage : PageBase
    {
        public EditInspectionPage(IWebDriver driver) : base(driver)
        {
        }

        public ViewInspectionPage ChangeStatus(string status)
        {
            var statusList = _driver.FindElementWait(By.XPath("(//div[@class='requiredInput']//select)[2]"));
            var statusField = new SelectElement(statusList);
            statusField.SelectByText(status);

            var saveButton = _driver.FindElementWait(By.XPath("((//td[@id='topButtonRow']//input)[1]"));
            saveButton.Click();
            var page = new ViewInspectionPage(_driver);
            return page;
        }
            
    }
}