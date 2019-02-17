using System.Collections.Generic;
using System.Linq;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.ApiClients;
using JobMatcher.Application.Interfaces.Services;

namespace JobMatcher.Application.Services.Jobs
{
    public class JobService : IJobService
    {
        private IJobClient _jobClient;
        private ICandidateClient _candidateClient;

        public JobService(IJobClient jobClient, ICandidateClient candidateClient)
        {
            _jobClient = jobClient;
            _candidateClient = candidateClient;
        }

        public IList<MatchedJob> GetJobsWithMatchedCandidates()
        {
            var jobsDto = _jobClient.GetAll();
            var candidatesDto = _candidateClient.GetAll();

            //Assumption only one Job and one candidate
            var jobDto = jobsDto.FirstOrDefault();

            var matchedJob = new MatchedJob
            {
                JobDto = jobDto
            };

            return new List<MatchedJob> {matchedJob};
        }
    }
}
