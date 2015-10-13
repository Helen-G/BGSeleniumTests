using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        protected abstract string GetPageUrl();
    }
}
