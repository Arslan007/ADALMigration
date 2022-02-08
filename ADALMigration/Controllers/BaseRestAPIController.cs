using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IRISNDT.Technicians.Web.Controllers.Restful
{
    [ApiController]
    [Authorize]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("api/[controller]")]
    public abstract class BaseRestAPIController : ControllerBase
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly ILogger logger;
        protected ILoggerFactory loggerFactory;

        protected BaseRestAPIController(
            ILoggerFactory loggerFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            this.loggerFactory = loggerFactory;
            logger = loggerFactory.CreateLogger(GetType().FullName);
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}