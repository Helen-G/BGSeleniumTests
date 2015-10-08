using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BGSeleniumTests
{
    public static class ExtensionMethods
    {
        public static IWebElement FindElementWait(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IEnumerable<IWebElement> foundElements = null;
            wait.Until(d =>
            {
                foundElements = driver.FindElements(by).Where(x => x.Displayed && x.Enabled);
                return foundElements.Count() != 0;
            });
            return foundElements.First();
        }

        public static string FindElementText(this IWebDriver driver, By by)
        {
            var text = FindElementWait(driver, by).Text;
            return text;
        }

        public static IWebElement OpenTab(this IWebDriver driver, string tabName)
        {
            var name = $"//a[@title=\'{tabName}\']";
            var element = driver.FindElementWait(By.XPath(name));
            element.Click();
            return element;
        }
    }

}
