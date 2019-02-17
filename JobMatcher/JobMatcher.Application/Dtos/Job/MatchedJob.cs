using System.Collections.Generic;

namespace JobMatcher.Application.Dtos.Job
{
    public class MatchedJob
    {
        public MatchedJob()
        {
            MatchedCandidatesDto = new List<MatchedCandidateDto>();
        }
        public JobDto JobDto { get; set; }
        public IList<MatchedCandidateDto> MatchedCandidatesDto { get; set; }
    }
}
