using System.Collections.Generic;
using JobAdder.Application.Dtos.Job;
using JobAdder.Application.Interfaces.Services;

namespace JobAdder.Application.Services.Jobs
{
    public class JobService : IJobService
    {
        public IList<MatchedJob> GetJobsWithMatchedCandidates()
        {
            return new List<MatchedJob>();
        }
    }
}
