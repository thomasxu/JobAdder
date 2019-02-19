using System.Collections.Generic;
using System.Linq;
using JobAdder.Domain.ApiClients.Jobs.Response;
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

        /// <summary>
        /// Get matched candidates by all jobs
        /// </summary>
        /// <returns></returns>
        public IList<MatchedJobDto> GetJobsWithMatchedCandidates()
        {
            //Data source of application jobs and candidates are coming from api call
            var jobsResponse = _jobClient.GetAll();
            var candidatesResponse = _candidateClient.GetAll();

            var matchedJobDtos = jobsResponse.Select(jobResponse => GetJobWithMatchedCandidates(jobResponse, candidatesResponse));

            return matchedJobDtos.ToList();
        }


        /// <summary>
        /// Get matched candidates by one job
        /// </summary>
        /// <param name="jobResponse"></param>
        /// <param name="candidatesResponse"></param>
        /// <returns></returns>
        private MatchedJobDto GetJobWithMatchedCandidates(JobResponse jobResponse, IList<CandidateResponse> candidatesResponse)
        {
            var jobSkills = ParseSkills(jobResponse.Skills);

            var matchedJobDto = new MatchedJobDto
            {
               JobId = jobResponse.JobId,
               Name = jobResponse.Name,
               Company = jobResponse.Company
            };

            var matchedCandidateDtos = candidatesResponse
                .Select(candidateResponse => ScoreCandidateBySkillMatch(candidateResponse, jobSkills))
                .Where(candidate => candidate != null)
                .OrderByDescending(candidate => candidate.MatchedScore)
                .ToList();

            matchedJobDto.MatchedCandidates = matchedCandidateDtos;

            return matchedJobDto;
        }


        /// <summary>
        /// Match job's skill with candidate's skill
        /// </summary>
        /// <param name="candidateResponse"></param>
        /// <param name="jobSkills"></param>
        /// <returns></returns>
        private MatchedCandidateDto ScoreCandidateBySkillMatch(CandidateResponse candidateResponse,
            IDictionary<string, int> jobSkills)
        {
            int score = 0;
            IList<string> matchedSkills = new List<string>();
            var candidateSkills = ParseSkills(candidateResponse.SkillTags);

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
                CandidateId = candidateResponse.CandidateId,
                Name = candidateResponse.Name,
                MatchedSkills = matchedSkills.ToArray(),
                MatchedScore = score
            };

            return matchedCandidateDto;
        }


        /// <summary>
        /// Parse and normalize the skill and give the score to each skill
        /// Algorithm:
        /// Order matters for both Job and Candidate.
        /// The first skill get score 100, the second get 99, the third get 98 .... for both Job and Candidate
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
