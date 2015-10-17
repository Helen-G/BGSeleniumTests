using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class ViewLicensePage : PageBase
    {
        public ViewLicensePage(IWebDriver driver) : base(driver) { }

        public string Name => _driver.FindElementText(By.XPath("//div[@class='content']/h2"));

        public List<string> Submissions
        {
            get
            {
                var result = new List<string>();
                var elements = _driver.FindElements(By.XPath("//a[contains(., 'Test Submission ')]"));
                if (elements.Count != 0)
                {
                    result.AddRange(elements.Select(element => element.Text));
                }
                return result;
            }
        }

        public string LicenseStatus => _driver.FindElementText(By.XPath("//td[text()='Status']/following-sibling::td/div"));

        public ViewFeePage OpenFeePage()
        {
            FeeNumber.Click();
            var page = new ViewFeePage(_driver);
            return page;
        }

        #region Fees list

        public IEnumerable<IWebElement> FeesList
            => _driver.FindElementsWait(By.XPath("//td[text()='Test License Fee']/preceding-sibling::th/a"));

        public string FeeType => _driver.FindElementText(By.XPath("//td[text()='Test License Fee']"));

        public string FeeAmount => _driver.FindElementText(By.XPath("(//td[text()='Test License Fee']/following-sibling::td)[1]"));

        public IWebElement FeeNumber => _driver.FindElement(By.XPath("//td[text()='Test License Fee']/preceding-sibling::th/a"));

        public string Receipt { get; set; }

#endregion

        public string InspectionType => _driver.FindElementText(By.XPath("//td[text()='Test Inspection']"));

        public IWebElement InspectionNumber => _driver.FindElementWait(By.XPath("//td[text()='Test Inspection']/preceding-sibling::th/a"));

        public ManageSubmissionsPage OpenManageSubmissionsPage()
        {
            var manageSubmissionsButton = _driver.FindElementWait(By.XPath("//input[@value='Manage Submissions']"));
            manageSubmissionsButton.Click();
            var manageSubmissionsPage = new ManageSubmissionsPage(_driver);
            return manageSubmissionsPage;
        }

        public ViewInspectionPage OpenViewInspectionPage()
        {
            InspectionNumber.Click();
            var page = new ViewInspectionPage(_driver);
            return page;
        }

    }
}