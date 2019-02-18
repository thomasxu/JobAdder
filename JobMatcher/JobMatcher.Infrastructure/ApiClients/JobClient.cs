using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.ApiClients;

namespace JobAdder.Infrastructure.ApiClients
{
    public class JobClient: IJobClient
    {
        private IApiClient _apiClient;

        public JobClient(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IList<JobDto> GetAll()
        {
            var response = _apiClient.Get<List<JobDto>>("jobs");
            return response;
        }
    }
}
