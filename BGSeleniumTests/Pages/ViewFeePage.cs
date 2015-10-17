using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class ViewFeePage : PageBase
    {
        public ViewFeePage(IWebDriver driver) : base(driver) { }

        public ViewCartPage OpenCartPage()
        {
            var cartLink = _driver.FindElementWait(By.XPath("//td[text()='Cart']/following-sibling::td/div/a"));
            cartLink.Click();
            var page = new ViewCartPage(_driver);
            return page;
        }
    }
}