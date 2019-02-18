using System;
using JobMatcher.Application.Interfaces.ApiClients;
using RestSharp;

namespace JobAdder.Infrastructure.ApiClients
{
    public class ApiClient: IApiClient
    {
        private const string ApiBaseUrl = "http://private-76432-jobadder1.apiary-mock.com";

        private readonly RestClient _client;

        public ApiClient()
        {
            _client = new RestClient(ApiBaseUrl);
        }

        public T Get<T>(string url = "") where T: new()
        {
            return Execute<T>(new RestRequest(url, Method.GET));
        }


        private T Execute<T>(RestRequest request) where T: new()
        {
            var response = _client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var apiClientException = new ApplicationException(message, response.ErrorException);
                throw apiClientException;
            }
            return response.Data;
        }

    }
}
