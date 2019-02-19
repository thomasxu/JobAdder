using System.Collections.Generic;
using JobAdder.Domain.ApiClients.Jobs.Response;
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

            IList<CandidateResponse> candidatesResposne = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "react, NodeJs"
                }
            };
            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, DotNetCore"
                }
            };

            candidateClient.GetAll().Returns(candidatesResposne);
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidatesDto = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidatesDto.Count);

            var matchedCandidatesDto = jobsWithMatchedCandidatesDto[0].MatchedCandidates;
            Assert.Equal(0, matchedCandidatesDto.Count);
        }

        [Fact]
        public void GetJobsWithMatchedCandidate_SkillCaseNotMatch_IsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateResponse> candidatesResponse = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "angular7, dotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidatesResponse);


            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, DotNetCore"
                }
            };
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidatesDto = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidatesDto.Count);

            var matchedCandidate = jobsWithMatchedCandidatesDto[0].MatchedCandidates;
            Assert.Equal(1, matchedCandidate.Count);
            Assert.Equal(1, matchedCandidate[0].CandidateId);
            Assert.Equal(2, matchedCandidate[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidate[0].MatchedScore);
        }


        [Fact]
        public void GetJobsWithMatchedCandidate_DuplicatedSkill_FistIsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateResponse> candidatesResponse = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "Angular7, DotNetCore, Anuglar7, DotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidatesResponse);


            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, Angular7, DotNetCore, DotNetCore"
                }
            };
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidate.Count);

            var matchedCandidatesResponse = jobsWithMatchedCandidate[0].MatchedCandidates;
            Assert.Equal(1, matchedCandidatesResponse.Count);
            Assert.Equal(2, matchedCandidatesResponse[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidatesResponse[0].MatchedScore);
        }

        [Fact]
        public void GetJobsWithMatchedCandidate_HasWhiteSpace_IsPickedWithRightScore()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateResponse> candidates = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Thomas",
                    SkillTags = "    Angular7    ,            DotNetCore   "
                }
            };
            candidateClient.GetAll().Returns(candidates);


            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "    Angular7     ,      DotNetCore    "
                }
            };
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidateDto = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidateDto.Count);

            var matchedCandidatesDto = jobsWithMatchedCandidateDto[0].MatchedCandidates;
            Assert.Equal(1, matchedCandidatesDto.Count);
            Assert.Equal(2, matchedCandidatesDto[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidatesDto[0].MatchedScore);
        }


        [Fact]
        public void GetJobsWithMatchedCandidate_MultipleCandidates_BestScoreOnTop()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateResponse> candidatesResponse = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Mr Potato",
                    SkillTags = "Angular7, React"
                },
                new CandidateResponse
                {
                    CandidateId = 2,
                    Name = "Mr Beans",
                    SkillTags = "React, DotNetCore"
                },
                new CandidateResponse
                {
                    CandidateId = 3,
                    Name = "Thomas",
                    SkillTags = "Angular7, DotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidatesResponse);


            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, DotNetCore"
                }
            };
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidatesDto = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(1, jobsWithMatchedCandidatesDto.Count);

            var matchedCandidatesDto = jobsWithMatchedCandidatesDto[0].MatchedCandidates;
            Assert.Equal(3, matchedCandidatesDto.Count);

            Assert.Equal(3, matchedCandidatesDto[0].CandidateId);
            Assert.Equal(2, matchedCandidatesDto[0].MatchedSkills.Length);
            Assert.Equal(398, matchedCandidatesDto[0].MatchedScore);

            Assert.Equal(1, matchedCandidatesDto[1].CandidateId);
            Assert.Single(matchedCandidatesDto[1].MatchedSkills);
            Assert.Equal(200, matchedCandidatesDto[1].MatchedScore);

            Assert.Equal(2, matchedCandidatesDto[2].CandidateId);
            Assert.Single(matchedCandidatesDto[2].MatchedSkills);
            Assert.Equal(198, matchedCandidatesDto[2].MatchedScore);
        }


        [Fact]
        public void GetJobsWithMatchedCandidate_MultipleJobMultipleCandidates_MatchCandidateToRightJob()
        {
            var candidateClient = Substitute.For<ICandidateClient>();
            var jobClient = Substitute.For<IJobClient>();

            IList<CandidateResponse> candidates = new List<CandidateResponse>
            {
                new CandidateResponse
                {
                    CandidateId = 1,
                    Name = "Mr Potato",
                    SkillTags = "Angular7, SqlServer"
                },
                new CandidateResponse
                {
                    CandidateId = 2,
                    Name = "Mr Beans",
                    SkillTags = "React, NodeJs"
                },
                new CandidateResponse
                {
                    CandidateId = 3,
                    Name = "Thomas",
                    SkillTags = "Angular7, DotNetCore"
                }
            };
            candidateClient.GetAll().Returns(candidates);


            IList<JobResponse> jobsResponse = new List<JobResponse>
            {
                new JobResponse
                {
                    JobId = 1,
                    Company = "JobAdder",
                    Skills = "Angular7, DotNetCore"
                },
                new JobResponse
                {
                    JobId = 2,
                    Company = "Company2",
                    Skills = "MVC, SqlServer"
                }
            };
            jobClient.GetAll().Returns(jobsResponse);


            var sut = new JobService(jobClient, candidateClient);
            var jobsWithMatchedCandidate = sut.GetJobsWithMatchedCandidates();

            Assert.Equal(2, jobsWithMatchedCandidate.Count);

            var company1Candidates = jobsWithMatchedCandidate[0].MatchedCandidates;
            Assert.Equal(2, company1Candidates.Count);
            company1Candidates[0].CandidateId = 3;
            company1Candidates[0].MatchedScore = 398;

            company1Candidates[1].CandidateId = 1;
            company1Candidates[1].MatchedScore = 200;

            var company2Candidates = jobsWithMatchedCandidate[1].MatchedCandidates;
            Assert.Equal(1, company2Candidates.Count);
            company2Candidates[0].CandidateId = 1;
            company2Candidates[0].MatchedScore = 198;
        }
    }
}
