namespace JobMatcher.Application.Dtos.Job
{
    public class MatchedCandidateDto
    {
        public int CandidateId { get; set; }

        public string Name { get; set; }
        public int MatchedScore { get; set; }
        public string[] MatchedSkills { get; set; }
    }
}
