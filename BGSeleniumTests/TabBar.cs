using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace BGSeleniumTests
{
    public class TabBar : PageBase
    {

        public TabBar(IWebDriver driver) : base(driver)
        {

        }

        public LicensesPage OpenLicensesPage()
        {
            //_driver.OpenTab("Licenses");
            return new LicensesPage(_driver);
        }

        protected override string GetPageUrl()
        {
            throw new NotImplementedException();
        }
    }
}
