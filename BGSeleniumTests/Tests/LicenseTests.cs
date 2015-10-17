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

        [Test, CategorySmoke]
        public void Can_complete_license_submission_and_pay_a_fee_via_cart()
        {
            //create a license of business type
            var licensesPage = _licensingAppPage.TabBar.OpenLicensesPage();
            var newLicensePage = licensesPage.OpenNewLicensePage();
            var viewLicensePage = newLicensePage.CreateLicense(type:"Business");

            var licensePageUrl = viewLicensePage.Url;

            //upload a submission file 
            viewLicensePage.OpenManageSubmissionsPage();
            _driver.UploadFile();

            Assert.AreEqual("Submitted", viewLicensePage.LicenseStatus);
            Assert.IsNotEmpty(viewLicensePage.FeesList);
            Assert.AreEqual("Test License Fee", viewLicensePage.FeeType);
            Assert.AreEqual("$100.00", viewLicensePage.FeeAmount);

            //pay a fee via cart, and check a generated receipt 
            var feePage = viewLicensePage.OpenFeePage();
            var cartPage = feePage.OpenCartPage();
            var generatedReceiptNumber = cartPage.PayFeeViaCart();

            Assert.IsNotEmpty(generatedReceiptNumber);
            Assert.IsNotEmpty(cartPage.PaidReceipt);

            //view a generated inspection record on the license details page 
            _driver.Navigate().GoToUrl(licensePageUrl);
            viewLicensePage = new ViewLicensePage(_driver);

            Assert.IsNotNull(viewLicensePage.InspectionNumber);
            Assert.AreEqual("Test Inspection", viewLicensePage.InspectionType);

            //open the inspection details page
            var viewInspectionPage = viewLicensePage.OpenViewInspectionPage();
            var checklistPage = viewInspectionPage.OpenChecklistPage();

            //verify that checklist page has 2 sections and 2 questions in each
            var sections = checklistPage.Elements(checklistPage.SectionsPath);
            Assert.AreEqual("Hand Washing", sections[0]);
            Assert.AreEqual("Food Safety", sections[1]);

            var questions = checklistPage.Elements(checklistPage.QuestionsPath);
            Assert.AreEqual(4, questions.Count);

            //complete the inspection and set status to Denied
            checklistPage.AnswerPassForAll();
            viewInspectionPage = checklistPage.CompleteChecklist();

            var editInspectionPage = viewInspectionPage.OpenEditInspectionPage();
            var viewLInspectionPage = editInspectionPage.ChangeStatus("Denied");

            Assert.AreEqual("Denied", viewLInspectionPage.Status);

            //view a generated re-inspection record on the license details page 
        }


    }
}
