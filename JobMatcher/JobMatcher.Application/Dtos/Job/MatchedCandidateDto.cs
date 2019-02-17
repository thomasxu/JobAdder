namespace JobMatcher.Application.Dtos.Job
{
    public class MatchedCandidateDto
    {
        public int MatchedScore { get; set; }
        public string[] MatchedSkills { get; set; }
        public CandidateDto Candidate { get; set; }
    }
}