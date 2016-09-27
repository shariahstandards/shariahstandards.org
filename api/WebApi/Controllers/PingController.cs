using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
    [RoutePrefix("api")]

    public class PingController : ApiController
    {
        private readonly IUserService _userService;

        public PingController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("ping")]
        [HttpGet]
        public IHttpActionResult Ping()
        {
            return Ok(new
                {
                    Message = "All good. You don't need to be authenticated to call this."
                }
            );
        }

        [Authorize]
        [Route("claims")]
        [HttpGet]
        public object Claims()
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return claimsIdentity.Claims.Select(c =>
            new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        [Authorize]
        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register(Auth0UserProfile profile)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;

            return Request.CreateResponse(HttpStatusCode.OK, _userService.TrackLogin(profile,claimsIdentity));
        }
    }
}