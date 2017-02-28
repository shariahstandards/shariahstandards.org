using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Services;
using WebApiResources;

namespace WebApi.Controllers
{
    public class SuggestionController : ApiController
    {
        private readonly ISuggestionService _service;

        public SuggestionController(ISuggestionService service)
        {
            _service = service;
        }

        [Route("CreateSuggestion")]
        public HttpResponseMessage Post(CreateSugestionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.CreateSuggestion(User, request));
        }
        [Route("DeleteSuggestion")]
        public HttpResponseMessage Post(DeleteSugestionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.DeleteSuggestion(User, request));
        }
        [Route("ViewSuggestion")]
        public HttpResponseMessage Post(ViewSugestionRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.ViewSuggestion(User, request));
        }
        [Route("SearchSuggestions")]
        public HttpResponseMessage Post(SearchSuggestionsRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.Search(User, request));
        }
        [Route("Vote")]
        public HttpResponseMessage Post(VoteOnSuggestionsRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.Vote(User, request));
        }
        [Route("RemoveVote")]
        public HttpResponseMessage Post(RemoveVoteOnSuggestionsRequest request)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _service.RemoveVote(User, request));
        }
        //[Route("UnhideSuggestion")]
        //public HttpResponseMessage Post(HideSuggestionsRequest request)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, _service.UnhideSuggestion(User, request));
        //}
        //[Route("HideSuggestion")]
        //public HttpResponseMessage Post(HideSuggestionsRequest request)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, _service.HideSuggestion(User, request));
        //}
        //[Route("VoteOnSuggestion")]
        //public HttpResponseMessage Post(VoteOnSuggestionsRequest request)
        //{
        //    return Request.CreateResponse(HttpStatusCode.OK, _service.VoteOnSuggestion(User, request));
        //}

    }
}
