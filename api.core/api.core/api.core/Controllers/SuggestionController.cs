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
    public class SuggestionController : Controller
    {
        private readonly ISuggestionService _service;

        public SuggestionController(ISuggestionService service)
        {
            _service = service;
        }

        [Route("api/CreateSuggestion")]
        public ResponseResource Post(CreateSugestionRequest request)
        {
            return _service.CreateSuggestion(User, request);
        }
        [Route("api/CommentOnSuggestion")]
        public ResponseResource Post(CreateSugestionCommentRequest request)
        {
            return _service.CommentOnSuggestion(User, request);
        }
        [Route("api/DeleteSuggestion")]
        public ResponseResource Post(DeleteSugestionRequest request)
        {
            return _service.DeleteSuggestion(User, request);
        }
        [Route("api/ViewSuggestion")]
        public SuggestionDetailResource Post(ViewSugestionRequest request)
        {
            return _service.ViewSuggestion(User, request);
        }
        [Route("api/SearchSuggestions")]
        public SearchSugestionsResponse Post(SearchSuggestionsRequest request)
        {
            return _service.Search(User, request);
        }
        [Route("api/Vote")]
        public ResponseResource Post(VoteOnSuggestionsRequest request)
        {
            return _service.Vote(User, request);
        }
        [Route("api/RemoveVote")]
        public ResponseResource Post(RemoveVoteOnSuggestionsRequest request)
        {
            return _service.RemoveVote(User, request);
        }
        //[Route("UnhideSuggestion")]
        //public HttpResponseMessage Post(HideSuggestionsRequest request)
        //{
        //    return _service.UnhideSuggestion(User, request));
        //}
        //[Route("HideSuggestion")]
        //public HttpResponseMessage Post(HideSuggestionsRequest request)
        //{
        //    return _service.HideSuggestion(User, request));
        //}
        //[Route("VoteOnSuggestion")]
        //public HttpResponseMessage Post(VoteOnSuggestionsRequest request)
        //{
        //    return _service.VoteOnSuggestion(User, request));
        //}

    }
}
