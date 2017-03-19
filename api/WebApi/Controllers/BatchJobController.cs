using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;

namespace WebApi.Controllers
{
    public class BatchJobController : ApiController
    {
        private readonly IBatchJobService _service;

        public BatchJobController(IBatchJobService service)
        {
            _service = service;
        }

        [Route("RunDailyCounts/{key}")]
        public HttpResponseMessage Get(string key)
        {
            _service.RunCounts(key);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
