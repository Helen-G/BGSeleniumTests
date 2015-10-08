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
    class LicensingAppTests : TestBase
    { 
        private LicensingAppPage _licensingAppPage;
        private TabBar _tabBar;

        [TestFixtureSetUp]
        public override void BeforeAll()
        {
            base.BeforeAll();
            _licensingAppPage = new LicensingAppPage(_driver);
            //_tabBar = new TabBar(_driver);
            //_licensesPage.NavigateTo();
        }


        [Test]
        public void Licensing_app_opens_after_login()
        {
            Assert.AreEqual("BG - Licensing", _licensingAppPage.AppMenuItem);

            var tabs = _licensingAppPage.DisplayedTabs();
            Assert.AreEqual("Home", tabs[0]);
            Assert.AreEqual("Licenses", tabs[1]);
            Assert.AreEqual("Contacts", tabs[2]);
            Assert.AreEqual("Accounts", tabs[3]);
            Assert.AreEqual("Reports", tabs[4]);
            Assert.AreEqual("Documents", tabs[5]);
            Assert.AreEqual("Help", tabs[6]);
        }

        [Test]
        public void Can_view_Licenses_list()
        {
            _driver.OpenTab("Licenses Tab");
            //var licensesTab = _licensingAppPage.TabBar.OpenLicensesPage();
            var licensesTab = _tabBar.OpenLicensesPage();
            Assert.AreEqual("Licenses", licensesTab.PageTitle);

        }
    }
}
