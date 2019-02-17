using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;

namespace JobMatcher.Application.Interfaces.Services
{
    public interface IJobService
    {
        IList<MatchedJob> GetJobsWithMatchedCandidates();
    }
}
