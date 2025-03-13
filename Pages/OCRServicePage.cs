using APIAutomation.APIHelper;
using APIAutomation.ExtentReport;
using AventStack.ExtentReports;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Pages
{
    public class OCRServicePage
    {
        private readonly APIHelperClass _APIHelper;
        public OCRServicePage()
        {
            _APIHelper = new APIHelperClass();
        }
        public async Task<RestResponse?> PostImageAnalyzeAsync(ExtentTest test)
        {
            var client = await _APIHelper.SetUrl("analyze-image");
            var request = await _APIHelper.CreatePostRequest();
            string imagePath = @"C:\\Users\\iray3\\Downloads\\2400811734.png";

            try
            {
                // Read Image as Byte Array
                byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);

                // Add Image as Binary Body
                request.AddParameter("application/octet-stream", imageBytes, ParameterType.RequestBody);

                // Execute Request
                return await client.ExecuteAsync(request);
            }
            catch (Exception ex)
            {
                ExtentReportManager.GetInstance().LogToReport(test, Status.Fail, $"{ex.Message}");
                return null;
            }
        }
    }
}
