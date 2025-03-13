using System;
using System.Net;
using System.Threading.Tasks;
using APIAutomation.ExtentReport;
using APIAutomation.Pages;
using AventStack.ExtentReports;
using Newtonsoft.Json;
using NUnit.Framework;
using Reqnroll;
using RestSharp;

namespace APIAutomation.StepDefinitions
{
    [Binding]
    public class OCRServiceStepDefinitions
    {
        private readonly OCRServicePage _OCRServicePage;

        private readonly OCRServiceStepDefinitions test;

        private RestResponse? _response;
        private readonly ScenarioContext _scenarioContext;
        public OCRServiceStepDefinitions(ScenarioContext scenarioContext)
        {
            _OCRServicePage = new OCRServicePage();
            _scenarioContext = scenarioContext;
        }
        [When("Send the request with a correct image as input")]
        public async Task WhenSendTheRequestWithACorrectImageAsInput()
        {
            var test = _scenarioContext.Get<ExtentTest>("CurrentTest");
            var step = ExtentReportManager.GetInstance().CreateTestStep(test, ScenarioStepContext.Current.StepInfo.Text.ToString());
            _response = await _OCRServicePage.PostImageAnalyzeAsync(step);           
            if (_response == null)
            {
                ExtentReportManager.GetInstance().LogToReport(step, Status.Fail, "POST request failed: Response is null");
                throw new Exception("POST request failed: Response is null");
            }
            else
            {
                ExtentReportManager.GetInstance().LogToReport(step, Status.Pass, "Sent POST request Successfully");
            }
        }

        [When("Verify the response")]
        public void WhenVerifyTheResponse()
        {
            var test = _scenarioContext.Get<ExtentTest>("CurrentTest");
            var step = ExtentReportManager.GetInstance().CreateTestStep(test, ScenarioStepContext.Current.StepInfo.Text.ToString());
            try
            {
                Assert.NotNull(_response, "Response should not be null");
                Assert.AreEqual(HttpStatusCode.OK, _response?.StatusCode, "Expected 200 OK status code");
                ExtentReportManager.GetInstance().LogToReport(step, Status.Pass, $"Statuscode : {_response?.StatusCode}");
            }
            catch (JsonException ex)
            {
                ExtentReportManager.GetInstance().LogToReport(step, Status.Fail, $"Error Message : {ex}");
            }
        }

        [Then("The response must contain a list of all the identified character strings.")]
        public void ThenTheResponseMustContainAListOfAllTheIdentifiedCharacterStrings_()
        {
            //Assert.NotNull(_response, "Response should not be null");
            //Assert.NotNull(_response?.Content, "Response content should not be null");

            var test = _scenarioContext.Get<ExtentTest>("CurrentTest");
            var step = ExtentReportManager.GetInstance().CreateTestStep(test, ScenarioStepContext.Current.StepInfo.Text.ToString());

            try
            {
                var result = JsonConvert.DeserializeObject<List<dynamic>>(_response.Content);
                Assert.NotNull(result, "Deserialized response should not be null");
                ExtentReportManager.GetInstance().LogToReport(step, Status.Pass, $"Image Analysis Data : {_response.Content}");
            }
            catch (JsonException ex)
            {
                Assert.Fail("Failed to deserialize JSON response: " + ex.Message);
                ExtentReportManager.GetInstance().LogToReport(step, Status.Fail, $"Image Analysis Data is Not found : {ex.Message}");
            }
        }
    }
}
