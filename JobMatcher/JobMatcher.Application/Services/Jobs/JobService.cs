using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.Services;

namespace JobMatcher.Application.Services.Jobs
{
    public class JobService : IJobService
    {
        public IList<MatchedJob> GetJobsWithMatchedCandidates()
        {
            return new List<MatchedJob>();
        }
    }
}
