using System.Collections.Generic;
using JobAdder.Application.Dtos.Job;

namespace JobAdder.Application.Interfaces.Services
{
    public interface IJobService
    {
        IList<MatchedJob> GetJobsWithMatchedCandidates();
    }
}
