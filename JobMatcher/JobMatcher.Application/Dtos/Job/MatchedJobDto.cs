using System.Collections.Generic;

namespace JobMatcher.Application.Dtos.Job
{
    public class MatchedJobDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public IList<MatchedCandidateDto> MatchedCandidates { get; set; }

        public MatchedJobDto()
        {
            MatchedCandidates = new List<MatchedCandidateDto>();
        }
    }
}
