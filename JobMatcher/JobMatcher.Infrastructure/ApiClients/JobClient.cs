using System.Collections.Generic;
using JobAdder.Domain.ApiClients.Jobs.Response;
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

        public IList<JobResponse> GetAll()
        {
            var response = _apiClient.Get<List<JobResponse>>("jobs");
            return response;
        }
    }
}
