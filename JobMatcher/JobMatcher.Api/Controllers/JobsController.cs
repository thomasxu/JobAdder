using JobAdder.Application.Interfaces.Services;
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
        public void GetJobsWithMatchedCandidates()
        {
            _jobService.GetJobsWithMatchedCandidates();
        }
    }
}
