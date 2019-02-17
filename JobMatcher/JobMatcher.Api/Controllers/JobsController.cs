using Microsoft.AspNetCore.Mvc;

namespace JobMatcher.Api.Controllers
{
    [Route("api/[controller]")]
    public class JobsController : Controller
    {
        [HttpGet]
        public void GetJobsWithMatchedCandidates()
        {
        }
    }
}
