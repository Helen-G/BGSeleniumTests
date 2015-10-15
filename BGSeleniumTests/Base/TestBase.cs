using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using BGSeleniumTests.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


namespace SeleniumTestsProject
{
    [TestFixture]
    public abstract class TestBase
    {
        protected IWebDriver _driver;
        private const string AppLogin = "masterrelease@basicgov.com";
        private const string AppPassword = "Cloudbench13c";

        [TestFixtureSetUp]
        public virtual void BeforeAll()
        {
            _driver = CreateChromeDriver();
            _driver.Manage().Window.Size = new Size(1200, 900);
            Login(AppLogin, AppPassword);
        }

        private LicensingAppPage Login(string login, string password)
        {
            _driver.Navigate().GoToUrl("https://login.salesforce.com/?un=masterrelease@basicgov.com&pw=Cloudbench13c");
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementExists(By.Id("home_Tab")));

            var licensesPage = new LicensingAppPage(_driver);
            return licensesPage;
        }

        [TestFixtureTearDown]
        public virtual void AfterAll()
        {
            _driver.Quit();
        }

        [TearDown]
        public void AfterEach()
        {
            //SaveScreenshot();
        }

        static IWebDriver CreateChromeDriver()
        {
            string chromeDriverPath = Environment.CurrentDirectory + @"\..\..\..\tools";
            var options = new ChromeOptions();
            options.AddArgument("--disable-extensions");
            return new ChromeDriver(chromeDriverPath, options);
        }

        public void SaveScreenshot()
        {
            if (_driver == null)
                return;
            var path = Environment.CurrentDirectory + @"\..\..\..\screenshots";
            Directory.CreateDirectory(path);
            var testName = TestContext.CurrentContext.Test.Name;
            var fileName = string.Format("{0:yyyy-MM-dd_hh-mm}-{1}.{2}", DateTime.Now, testName, "png");
            var fullPath = Path.Combine(path, fileName);

            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            Thread.Sleep(500);
            screenshot.SaveAsFile(fullPath, ImageFormat.Png);
            Thread.Sleep(500);
        }

    }
}
