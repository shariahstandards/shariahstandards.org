using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
    //[RoutePrefix("api")]

    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;

        public UserProfileController(IUserService userService)
        {
            _userService = userService;
        }

        //[Route("ping")]
        //[HttpGet]
        //public IHttpActionResult Ping()
        //{
        //    return Ok(new
        //        {
        //            Message = "All good. You don't need to be authenticated to call this."
        //        }
        //    );
        //}

        //[Authorize]
        //[Route("claims")]
        //[HttpGet]
        //public object Claims()
        //{
        //    var claimsIdentity = User.Identity as ClaimsIdentity;

        //    return claimsIdentity.Claims.Select(c =>
        //    new
        //    {
        //        Type = c.Type,
        //        Value = c.Value
        //    });
        //}

       // [Authorize]
        [Route("api/UserProfile")]
        public UserProfileResource PostFullUserProfileRequest(Auth0UserProfile profile)
        {
            return _userService.GetUserProfile(profile,User);
        }
    }
}
