using Microsoft.AspNetCore.Mvc;
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
  public class HumanController : Controller
  {
    private IHumanService _service;
    public HumanController(IHumanService service)
    {
      _service = service;
    }
    [Route("api/RegisterAHumanBeing")]
    [HttpPost]
    public ResponseResource RegisterAHumanBeing([FromBody]RegisterAHumanBeingRequest request)
    {
      return _service.RegisterAHumanBeing(User, request);
    }
  }
}
