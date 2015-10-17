using OpenQA.Selenium;

namespace BGSeleniumTests.Pages
{
    public class ViewCheckListPage : PageBase
    {
        public ViewCheckListPage(IWebDriver driver) : base(driver) {}

        public By QuestionsPath => By.XPath("//table[@class='htmlDetailElementTable']//div");

        public By SectionsPath => By.XPath("//h3");

        public By RadioButtonsPath => By.XPath("//label[text()=' Pass']/preceding-sibling::input");

        public void AnswerPassForAll()
        {
            var elements = _driver.FindElementsWait(RadioButtonsPath);
            foreach (var element in elements)
            {
                element.Click();
            }
        }

        public ViewInspectionPage CompleteChecklist()
        {
            var saveAndCompleteButton = _driver.FindElementWait(By.XPath("//input[text()='checklistPage']"));
            saveAndCompleteButton.Click();
            _driver.SwitchTo().Alert().Accept();
            var page = new ViewInspectionPage(_driver);
            return page;
        }

    }
}