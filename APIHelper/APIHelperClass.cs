using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.APIHelper
{
    public class APIHelperClass
    {
        private readonly string baseUrl = "https://dev.europe.api.apt.gn.com/ocr-service/v1/";
        private readonly RestClient restClient;

        public APIHelperClass()
        {
            restClient = new RestClient(baseUrl);
        }
        public Task<RestClient> SetUrl(string endpoint)
        {
            var url = Path.Combine(baseUrl, endpoint);
            return Task.FromResult(new RestClient(url));
        }
        public Task<RestRequest> CreatePostRequest()
        {
            var request = new RestRequest { Method = Method.Post };
            // Add Headers
            request.AddHeader("Ocp-Apim-Subscription-Key", "39731117349c436792eca8513c7d2eb6");
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/octet-stream"); // Binary format
            request.AddHeader("username", "surya");
            request.AddHeader("machinename", "FSWIRAY112");
            request.AddHeader("site", "99");
            return Task.FromResult(request);
        }
    }
}
