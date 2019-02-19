using System.Collections.Generic;
using JobAdder.Domain.ApiClients.Jobs.Response;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.ApiClients;

namespace JobAdder.Infrastructure.ApiClients
{
    public class CandidateClient: ICandidateClient
    {
        private IApiClient _apiClient;

        public CandidateClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IList<CandidateResponse> GetAll()
        {
            var response = _apiClient.Get<List<CandidateResponse>>("candidates");
            return response;
        }
    }
}
