using APIAutomation.ExtentReport;
using AventStack.ExtentReports;
using Reqnroll;

namespace APIAutomation.Hooks
{
    [Binding]
    public sealed class ReqnrollHooks
    {
        // For additional details on Reqnroll hooks see https://go.reqnroll.net/doc-hooks

        private static ExtentReportManager _reportManager;
        private static ExtentTest _currentTest;

        [BeforeTestRun]
        public static void Setup()
        {
            _reportManager = ExtentReportManager.GetInstance();
        }

        //[BeforeScenario("@tag1")]
        //public void BeforeScenarioWithTag()
        //{
        //    // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
        //    // See https://go.reqnroll.net/doc-hooks#tag-scoping

        //    //TODO: implement logic that has to run before executing each scenario
        //}

        [BeforeScenario]
        public void FirstBeforeScenario(ScenarioContext scenarioContext)
        {
            // Example of ordering the execution of hooks
            // See https://go.reqnroll.net/doc-hooks#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario

            _currentTest = _reportManager.CreateTest(scenarioContext.ScenarioInfo.Title);
            scenarioContext.Set(_currentTest, "CurrentTest");
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }

        [AfterTestRun]
        public static void TearDown()
        {
            _reportManager.FlushReport();
        }
    }
}