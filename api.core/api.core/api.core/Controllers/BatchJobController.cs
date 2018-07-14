using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
  public class BatchJobController : Controller
  {
    private readonly IBatchJobService _service;

    public BatchJobController(IBatchJobService service)
    {
      _service = service;
    }

    [Route("api/RunDailyCounts/{key}")]
    [ProducesResponseType(200, Type = typeof(BatchJobResultResource))]
    [ProducesResponseType(404)]
    public IActionResult Get(string key)
    {
      if (key == "bbhrglkjgilksgyw798987874865211642")
      {
        return Ok(_service.RunCounts());
      }
      return NotFound();
    }
  }
}
