using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiResources;

namespace WebApi.Controllers
{

  public class QuranSearchController : Controller
  {
    private IQuranSearchService _service;
    public QuranSearchController(IQuranSearchService service)
    {
      _service = service;
    }

    //[Route("QuranVerse/{surahNumber}/{verseNumber}/{wordNumber}/{wordPartNumber}")]
    //[Route("QuranVerse/{surahNumber}/{verseNumber}/{wordNumber}")]onSearchResultSelected
    [Route("api/QuranVerse/{surahNumber}/{verseNumber}")]
    [HttpGet]
    public VerseResponse GetVerse(int surahNumber, int verseNumber )
    {
      return _service.GetVerse(surahNumber,verseNumber);
    }
    [Route("api/Surahs")]
    [HttpGet]
    public List<SurahInformationResponse> GetSurahs()
    {
      return _service.GetSurahs();
    }
    [Route("api/SearchQuran")]
    [HttpPost]
    public QuranSearchResult Search([FromBody]QuranSearchRequest request)
    {
      return _service.Search(request);
    }
  }
}
