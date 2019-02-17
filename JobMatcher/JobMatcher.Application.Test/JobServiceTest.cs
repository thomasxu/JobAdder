using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.ApiClients;
using JobMatcher.Application.Services.Jobs;
using NSubstitute;
using Xunit;

namespace JobMatcher.Application.Test
{
    public class JobServiceTest
    {
        [Fact]
        public void GetJobsWithMatchedCandidates_SkillNotMatch_IsNotPicked()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateDto> candidates = new List<CandidateDto>
            {
                new CandidateDto
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "react, NodeJs"
                }
            };

            IList<JobDto> jobs = new List<JobDto>
            {
                new JobDto
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, DotNetCore"
                }
            };

            jobClient.GetAll().Returns(jobs);
            candidateClient.GetAll().Returns(candidates);

            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidate = jobsWithMatchedCandidate[0].MatchedCandidatesDto;
            Assert.Equal(0, matchedCandidate.Count);
        }



        [Fact]
        public void GetJobsWithMatchedCandidate_SkillMatch_IsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateDto> candidates = new List<CandidateDto>
            {
                new CandidateDto
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "Angular7"
                }
            };
            IList<JobDto> jobs = new List<JobDto>
            {
                new JobDto
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7"
                }
            };

            candidateClient.GetAll().Returns(candidates);
            jobClient.GetAll().Returns(jobs);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidate = jobsWithMatchedCandidate[0].MatchedCandidatesDto;
            Assert.Equal(1, matchedCandidate.Count);
            Assert.Equal(1, matchedCandidate[0].Candidate.CandidateId);
            Assert.Single(matchedCandidate[0].MatchedSkills);
            Assert.Equal(200, matchedCandidate[0].MatchedScore);
        }
    }
}
