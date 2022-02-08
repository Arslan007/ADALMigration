using Microsoft.AspNetCore.Mvc;

namespace IRISNDT.Technicians.Web.Controllers.Restful
{
    public class JobController : BaseRestAPIController
    {
        public JobController(
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor) : base(loggerFactory, httpContextAccessor)
        {
        }

        [HttpGet("HelloWorld")]
        public string GetAsync()
        {
            return "Hello World";
        }
    }
}