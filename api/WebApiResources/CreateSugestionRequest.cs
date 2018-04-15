using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class CreateSugestionRequest
    {
        public int OrganisationId { get; set; }
        public string Subject { get; set; }
        public string Suggestion { get; set; }
    }
    public class DeleteSugestionRequest
    {
        public int SuggestionId { get; set; }
    }

    public class CreateSugestionCommentRequest
    {
        public int SuggestionId { get; set; }
        public string Comment { get; set; }
        public bool? Supporting { get; set; }
    }
    public class ViewSugestionRequest
    {
        public int SuggestionId { get; set; }
    }

    public class VoteOnSuggestionsRequest
    {
        public int SuggestionId { get; set; }
        public bool? VotingInSupport { get; set; }
    }

    public class RemoveVoteOnSuggestionsRequest
    {
        public int VoteId { get; set; }
    }

}
