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

        public string Status => _driver.FindElementText(By.XPath("//td[text()='Status']/following-sibling::td/div"));

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

#region Fees list

        public IEnumerable<IWebElement> FeesList
            => _driver.FindElementsWait(By.XPath("//td[text()='Test License Fee']/preceding-sibling::th/a"));

        public string FeeType => _driver.FindElementText(By.XPath("//td[text()='Test License Fee']"));

        public string FeeAmount => _driver.FindElementText(By.XPath("(//td[text()='Test License Fee']/following-sibling::td)[1]"));

        public IWebElement FeeNumber => _driver.FindElement(By.XPath("//td[text()='Test License Fee']/preceding-sibling::th/a"));

        public string Receipt { get; set; }

#endregion

#region Inspection list

        public string InspectionType => _driver.FindElementText(By.XPath("//td[text()='Test Inspection']"));

        public IWebElement InspectionNumber => _driver.FindElementWait(By.XPath("//td[text()='Test Inspection']/preceding-sibling::th/a"));

        public IWebElement ReinspectionNumber => _driver.FindElementWait(By.XPath("//td[text()='Reinspection']/preceding-sibling::th/a"));

        public string Attachement
            => _driver.FindElementText(By.XPath("(//th[text()='Attachment']/following-sibling::td/a)[1]"));

        #endregion

        public ViewFeePage OpenFeePage()
        {
            FeeNumber.Click();
            var page = new ViewFeePage(_driver);
            return page;
        }

        public ManageSubmissionsPage OpenManageSubmissionsPage()
        {
            var manageSubmissionsButton = _driver.FindElementWait(By.XPath("//input[@value='Manage Submissions']"));
            manageSubmissionsButton.Click();
            var manageSubmissionsPage = new ManageSubmissionsPage(_driver);
            return manageSubmissionsPage;
        }

        public ViewInspectionPage OpenInspectionDetailsPage()
        {
            InspectionNumber.Click();
            var page = new ViewInspectionPage(_driver);
            return page;
        }

        public ViewInspectionPage OpenReinspectionDetailsPage(string reinspectionNumber)
        {
            ClickObjectUrlInRelatedList(reinspectionNumber);
            var page = new ViewInspectionPage(_driver);
            return page;
        }
    }
}