using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGSeleniumTests.Pages;
using NUnit.Framework;
using SeleniumTestsProject;

namespace BGSeleniumTests.Tests
{
    class LicenseTests : TestBase
    {
        private LicensesPage _licensesPage;

        [TestFixtureSetUp]
        public override void BeforeAll()
        {
            base.BeforeAll();
            _licensesPage = new LicensesPage(_driver);
            //_licensesPage.NavigateTo();
        }


        [Test]
        public void Licensing_app_opens_after_login()
        {
           Assert.AreEqual("BG - Licensing", _licensesPage.GetAppMenuItem);
        }
    }
}
