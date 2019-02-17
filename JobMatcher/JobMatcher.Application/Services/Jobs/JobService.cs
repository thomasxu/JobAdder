using System.Collections.Generic;
using System.Linq;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.ApiClients;
using JobMatcher.Application.Interfaces.Services;

namespace JobMatcher.Application.Services.Jobs
{
    public class JobService : IJobService
    {
        private IJobClient _jobClient;
        private ICandidateClient _candidateClient;

        public JobService(IJobClient jobClient, ICandidateClient candidateClient)
        {
            _jobClient = jobClient;
            _candidateClient = candidateClient;
        }

        public IList<MatchedJob> GetJobsWithMatchedCandidates()
        {
            var jobsDto = _jobClient.GetAll();
            var candidatesDto = _candidateClient.GetAll();

            //Assumption only one Job and one candidate
            var jobDto = jobsDto.FirstOrDefault();

            var matchedJob = new MatchedJob();
            var matchedCandidate = GetMatchedCandidateForJob(jobDto, candidatesDto.FirstOrDefault());
            if (matchedCandidate != null)
            {
                matchedJob.MatchedCandidatesDto.Add(matchedCandidate);
            }

            return new List<MatchedJob> {matchedJob};
        }


        private MatchedCandidateDto GetMatchedCandidateForJob(JobDto jobDto, CandidateDto candidateDto)
        {
            int score = 0;
            IList<string> matchedSkills = new List<string>();

            //Assumption: Both job and candidate has 1 skill
            //Algorithm order matters for both Job and Candidate.
            //The first skill get score 100, the second 99 .... for both Job and Candidate
            //If a match is found final JobMatch score is jobScore + candidateScore for the match,
            //the higher the score the better the match
            if (jobDto.Skills == candidateDto.SkillTags)
            {
                matchedSkills.Add(jobDto.Skills);
                score += 100 + 100;
            }

            if (matchedSkills.Count == 0)
            {
                return null;
            }

            var matchedCandidateDto = new MatchedCandidateDto
            {
                Candidate = candidateDto,
                MatchedSkills = matchedSkills.ToArray(),
                MatchedScore = score
            };

            return matchedCandidateDto;
        }
    }
}
