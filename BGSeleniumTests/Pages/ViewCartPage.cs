using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BGSeleniumTests.Pages
{
    public class ViewCartPage : PageBase
    {
        public ViewCartPage(IWebDriver driver): base(driver) { }
        public IEnumerable<IWebElement> ReceiptList
            => _driver.FindElementsWait(By.XPath("//td[text()='Test License Fee']" +
                                                 "/preceding-sibling::th/a"));

        public string PaidReceipt
            =>
                _driver.FindElementText(
                    By.XPath("//div[@class='listRelatedObject Custom16Block']" +
                             "//div[@class='pbBody']//th[1]"));

        public string PayFeeViaCart()
        {
            string mainWindow = _driver.CurrentWindowHandle;
            var finder = new PopupWindowFinder(_driver);

            //click 'Pay Cart' button
            var payCartButton = _driver.FindElementWait(By.XPath("//td[@id='topButtonRow']" +
                                                                 "/input[@value='Pay Cart']"));

            var cartWindowHandle = finder.Click(payCartButton);
            _driver.SwitchTo().Window(cartWindowHandle);

            //click 'Continue' button
            var continueButton = _driver.FindElementWait(By.XPath("//span[text()='Continue']"));
            continueButton.Click();

            //click 'Process Payment' button
            var processPaymentButton = _driver.FindElementWait(By.XPath("//input[@value='Process Payment']"));
            processPaymentButton.Click();

            //get generated receipt number
            var receiptNumber = _driver.FindElementText(
                By.XPath("//td[@class='data2Col first']//tr[contains(., 'Receipt Number')]/td[2]"));

            _driver.Close();
            _driver.SwitchTo().Window(mainWindow);
            _driver.Navigate().Refresh();
            return receiptNumber;
        }

    }
}