using System.Collections.Generic;
using JobMatcher.Application.Dtos.Job;
using JobMatcher.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobMatcher.Api.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        private IJobService _jobService;

        public JobsController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public IList<MatchedJobDto> GetJobsWithMatchedCandidates()
        {
            return _jobService.GetJobsWithMatchedCandidates();
        }
    }
}
