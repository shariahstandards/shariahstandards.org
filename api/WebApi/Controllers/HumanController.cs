using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiResources;

namespace WebApi.Controllers
{
    public class HumanController : ApiController
    {
        private IHumanService _service;
        public HumanController(IHumanService service)
        {
            _service = service;
        }
        [Route("RegisterAHumanBeing")]
        [HttpPost]
        public HttpResponseMessage RegisterAHumanBeing(RegisterAHumanBeingRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.RegisterAHumanBeing(User, request));
        }
    }
}
