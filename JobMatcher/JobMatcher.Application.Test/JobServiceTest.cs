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

            candidateClient.GetAll().Returns(candidates);
            jobClient.GetAll().Returns(jobs);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidates = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidates.Count);

            var matchedCandidate = jobsWithMatchedCandidates[0].MatchedCandidatesDto;
            Assert.Equal(0, matchedCandidate.Count);
        }

        [Fact]
        public void GetJobsWithMatchedCandidate_SkillCaseNotMatch_IsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateDto> candidates = new List<CandidateDto>
            {
                new CandidateDto
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "angular7, dotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidates);


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


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidate = jobsWithMatchedCandidate[0].MatchedCandidatesDto;
            Assert.Equal(1, matchedCandidate.Count);
            Assert.Equal(1, matchedCandidate[0].Candidate.CandidateId);
            Assert.Equal(2, matchedCandidate[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidate[0].MatchedScore);
        }


        [Fact]
        public void GetJobsWithMatchedCandidate_DuplicatedSkill_FistIsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateDto> candidates = new List<CandidateDto>
            {
                new CandidateDto
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "Angular7, DotNetCore, Anuglar7, DotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidates);


            IList<JobDto> jobs = new List<JobDto>
            {
                new JobDto
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, Angular7, DotNetCore, DotNetCore"
                }
            };
            jobClient.GetAll().Returns(jobs);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidate = jobsWithMatchedCandidate[0].MatchedCandidatesDto;
            Assert.Equal(1, matchedCandidate.Count);
            Assert.Equal(2, matchedCandidate[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidate[0].MatchedScore);
        }

        [Fact]
        public void GetJobsWithMatchedCandidate_HasWhiteSpace_IsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateDto> candidates = new List<CandidateDto>
            {
                new CandidateDto
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "    Angular7    ,            DotNetCore   "
                }
            };
            candidateClient.GetAll().Returns(candidates);


            IList<JobDto> jobs = new List<JobDto>
            {
                new JobDto
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "    Angular7     ,      DotNetCore    "
                }
            };
            jobClient.GetAll().Returns(jobs);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidate = jobsWithMatchedCandidate[0].MatchedCandidatesDto;
            Assert.Equal(1, matchedCandidate.Count);
            Assert.Equal(2, matchedCandidate[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidate[0].MatchedScore);
        }
    }
}
