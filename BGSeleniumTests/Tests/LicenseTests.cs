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

        [TestFixtureSetUp]
        public override void BeforeAll()
        {
            base.BeforeAll();
            _licensingAppPage = new LicensingAppPage(_driver);
        }

        [Test]
        public void Licensing_app_opens_after_login()
        {
            var tabs = _licensingAppPage.DisplayedTabs();

            Assert.AreEqual("BG - Licensing", _licensingAppPage.AppMenuItem);
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
            var licensesPage = _licensingAppPage.TabBar.OpenLicensesPage();

            Assert.AreEqual("Licenses", licensesPage.PageTitle);
            Assert.AreEqual("All", licensesPage.CurrentView);
        }

        [Test]
        public void Can_create_a_license_of_business_type_and_view_two_generated_submissions()
        {
            //create a license
            var licensesPage = _licensingAppPage.TabBar.OpenLicensesPage();
            var newLicensePage = licensesPage.OpenNewLicensePage();
            var viewLicensePage = newLicensePage.CreateLicense("Business");

            //verify the license name
            var currentDate = DateTime.Now.ToString("yyMM");
            Assert.That(viewLicensePage.Name, Is.StringContaining(currentDate));

            //verify the names of two automatically generated submissions
            Assert.AreEqual("Test Submission 2", viewLicensePage.Submissions[0]);
            Assert.AreEqual("Test Submission 1", viewLicensePage.Submissions[1]);
        }

        [Test]
        public void Can_upload_a_file_to_complete_license_submission_and_view_a_fee()
        {
            //create a license
            var licensesPage = _licensingAppPage.TabBar.OpenLicensesPage();
            var newLicensePage = licensesPage.OpenNewLicensePage();
            var viewLicensePage = newLicensePage.CreateLicense("Business");

            //upload a submission file 
            var manageSubmissionsPage = viewLicensePage.OpenManageSubmissionsPage();
            _driver.UploadFile();

            Assert.AreEqual("Submitted", viewLicensePage.LicenseStatus);
            Assert.IsNotEmpty(viewLicensePage.FeesList);
            Assert.AreEqual("Test License Fee", viewLicensePage.LicenseFeeType);
            Assert.AreEqual("$100.00", viewLicensePage.LicenseFeeAmount);
        }


    }
}
