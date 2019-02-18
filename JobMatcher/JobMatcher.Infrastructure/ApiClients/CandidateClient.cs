using System.Collections.Generic;
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

        public IList<CandidateDto> GetAll()
        {
            var response = _apiClient.Get<List<CandidateDto>>("candidates");
            return response;
        }
    }
}
