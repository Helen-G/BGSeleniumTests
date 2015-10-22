using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class ViewInspectionPage : PageBase
    {
        public ViewInspectionPage(IWebDriver driver) : base(driver) {}

        public string Title => _driver.FindElementText(By.XPath("//h2[@class='pageDescription']"));

        public string Status => _driver.FindElementText(By.XPath("//td[text()='Status']/following-sibling::td/div"));

        public string ReinspectionNumber => _driver.FindElementText(By.XPath("//td[text()='Reinspection']/preceding-sibling::th/a"));

        public string ReinspectionType => _driver.FindElementText(By.XPath("//td[text()='Reinspection']"));

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

        public ViewInspectionPage OpenReinspectionDetailsPage(string reinspectionNumber)
        {
            var path = $"//a[text()='{reinspectionNumber}']";
            var reinspectionNumberUrl = _driver.FindElementWait(By.XPath(path));
            reinspectionNumberUrl.Click();
            var page = new ViewInspectionPage(_driver);
            return page;
        }
    }

}