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
            var jobSkills = ParseSkills(jobDto.Skills);

            var matchedJob = new MatchedJob();
            var matchedCandidate = GetMatchedCandidateForJob(jobDto, candidatesDto.FirstOrDefault(), jobSkills);
            if (matchedCandidate != null)
            {
                matchedJob.MatchedCandidatesDto.Add(matchedCandidate);
            }

            return new List<MatchedJob> {matchedJob};
        }


        private MatchedCandidateDto GetMatchedCandidateForJob(JobDto jobDto, CandidateDto candidateDto,
            IDictionary<string, int> jobSkills)
        {
            int score = 0;
            IList<string> matchedSkills = new List<string>();
            var candidateSkills = ParseSkills(candidateDto.SkillTags);


            foreach (var jobSkill in jobSkills)
            {

                if (candidateSkills.ContainsKey(jobSkill.Key))
                {
                    matchedSkills.Add(jobSkill.Key);
                    score += jobSkill.Value + candidateSkills[jobSkill.Key];
                }
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


   

        /// <summary>
        /// Algorithm:
        /// Order matters for both Job and Candidate.
        /// The first skill get score 100, the second get 99, the third get 98 .... for both Job and Candidate
        /// If a match is found final JobMatch score is jobScore + candidateScore for the match
        /// The higher the score the better the match
        /// </summary>
        /// <param name="skills"></param>
        /// <returns></returns>
        private static Dictionary<string, int> ParseSkills(string skills)
        {
            const string separator = ",";
            int index = 0;
            var skillsScore = skills
                .Split(separator)
                .Select(js => js.Trim().ToLower())
                .Distinct()
                .ToDictionary(js => js, js => 100 - index++);

            return skillsScore;
        }
    }
}
