using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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

        public static IEnumerable<IWebElement> FindElementsWait(this IWebDriver driver, By by)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            IEnumerable<IWebElement> foundElements = null;
            wait.Until(d =>
            {
                foundElements = driver.FindElements(by).Where(x => x.Displayed && x.Enabled);
                return foundElements.Count() != 0;
            });
            return foundElements;
        }

        public static string FindElementText(this IWebDriver driver, By by)
        {
            var text = FindElementWait(driver, by).Text;
            return text;
        }

        public static void UploadFile(this IWebDriver driver, string buttonId = null)
        {
            //click Upload link to display Choose File button
            var uploadLink = driver.FindElementWait(By.XPath("(//td/a[text()='Upload'])[1]"));
            uploadLink.Click();

            Thread.Sleep(TimeSpan.FromSeconds(5));

            //Click Choose File button and select a file
            driver.SwitchTo().Frame("submissionUploadframe");
            var chooseFileButton = driver.FindElementWait(By.Id("chatterFile"));
            chooseFileButton.SendKeys("C:/Projects/BGSeleniumTests/tools/test_doc.txt");

#region code to try
            //select a file

            //doesn't click the file !
            //String script = "document.getElementById('chatterFile').value='" + "C:/Projects/BGSeleniumTests/tools/test_doc.txt" + "';";
            //((IJavaScriptExecutor)driver).ExecuteScript(script);

            //doesn't work !
            //SendKeys.SendWait("C:/Projects/BGSeleniumTests/tools/test_doc.txt");
            //SendKeys.SendWait(@"{Enter}");
#endregion

            //click Upload button
            var uploadButton = driver.FindElementWait(By.Id("publishersharebutton"));
            uploadButton.Click();

            Thread.Sleep(TimeSpan.FromSeconds(5));
            driver.SwitchTo().DefaultContent();
            var doneButton = driver.FindElementWait(By.XPath("//input[@value='Done']"));
            doneButton.Click();
        }

        

}
}
