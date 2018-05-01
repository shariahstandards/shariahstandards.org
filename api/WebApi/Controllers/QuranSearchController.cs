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

  public class QuranSearchController : ApiController
  {
    private IQuranSearchService _service;
    public QuranSearchController(IQuranSearchService service)
    {
      _service = service;
    }

    //[Route("QuranVerse/{surahNumber}/{verseNumber}/{wordNumber}/{wordPartNumber}")]
    //[Route("QuranVerse/{surahNumber}/{verseNumber}/{wordNumber}")]onSearchResultSelected
    [Route("QuranVerse/{surahNumber}/{verseNumber}")]
    [HttpGet]
    public HttpResponseMessage GetVerse(int surahNumber, int verseNumber )
    {
      return Request.CreateResponse(HttpStatusCode.OK, _service.GetVerse(surahNumber,verseNumber));
    }
    [Route("Surahs")]
    [HttpGet]
    public HttpResponseMessage GetSurahs()
    {
      return Request.CreateResponse(HttpStatusCode.OK, _service.GetSurahs());
    }
    [Route("SearchQuran")]
    [HttpPost]
    public HttpResponseMessage Search(QuranSearchRequest request)
    {
      return Request.CreateResponse(HttpStatusCode.OK, _service.Search(request.SearchText));
    }
  }
}
