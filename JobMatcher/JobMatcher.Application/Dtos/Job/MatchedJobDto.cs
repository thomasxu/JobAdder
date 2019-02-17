using System.Collections.Generic;

namespace JobMatcher.Application.Dtos.Job
{
    public class MatchedJobDto
    {
        public MatchedJobDto()
        {
            MatchedCandidatesDto = new List<MatchedCandidateDto>();
        }
        public JobDto JobDto { get; set; }
        public IList<MatchedCandidateDto> MatchedCandidatesDto { get; set; }
    }
}
