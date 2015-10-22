using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGSeleniumTests.Pages;
using OpenQA.Selenium;

namespace BGSeleniumTests
{
    public abstract class PageBase
    {
        protected IWebDriver _driver;

        protected PageBase(IWebDriver driver)
        {
            _driver = driver;
        }

        public TabBar TabBar => new TabBar(_driver);

        public string Url
        {
            get { return _driver.Url; }
        }

        public virtual void NavigateTo()
        {
            var url = "https://landmanagement.my.salesforce.com/" + GetPageUrl();
            _driver.Navigate().GoToUrl(url);
        }

        protected virtual string GetPageUrl()
        {
            return null;
        }

        public List<string> Elements(By by)
        {
            var result = new List<string>();
            var elements = _driver.FindElements(by);
            if (elements.Count != 0)
            {
                result.AddRange(elements.Select(element => element.Text));
            }
            return result;
        }

        protected void ClickObjectUrlInRelatedList(string objectName)
        {
            var path = $"//a[text()='{objectName}']";
            var objectUrlInRelatedList = _driver.FindElementWait(By.XPath(path));
            objectUrlInRelatedList.Click();
        }

        public ViewLicensePage OpenViewLicensePage(string pageUrl)
        {
            _driver.Navigate().GoToUrl(pageUrl);
            var page = new ViewLicensePage(_driver);
            return page;
        }
    }
}
