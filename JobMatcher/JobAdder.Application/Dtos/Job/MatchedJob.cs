using System.Collections.Generic;
using JobAdder.Application.Services.Jobs;

namespace JobAdder.Application.Dtos.Job
{
    public class MatchedJob
    {
        public JobDto JobDto { get; set; }
        public IList<MatchedCandidateDto> MatchedCandidatesDto { get; set; }
    }
}