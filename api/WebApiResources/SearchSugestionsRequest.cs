using System.Collections.Generic;

namespace WebApiResources
{
    public class SearchSuggestionsRequest
    {
        public int OrganisationId { get; set; }
        public int? Page { get; set; }
    }

    public class SearchSugestionsResponse:ResponseResource
    {
        public int OrganisationId { get; set; }
        public int PageCount { get; set; }
        public List<SuggestionSummaryResource> Suggestions { get; set; } 
    }

    public class SuggestionSummaryResource
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string DateTimeText { get; set; }
        public double VotingPercent { get; set; }
        public double NetSupportPercent { get; set; }
        public int AuthorMemberId { get; set; }
        public string AuthorPublicName { get; set; }
        public string AuthorPictureUrl { get; set; }
    }

    public class SuggestionDetailResource:ResponseResource
    {
        public SuggestionSummaryResource SuggestionSummary { get; set; }
        public string Suggestion { get; set; }
        public int VotesFor { get; set; }
        public int VotesAgainst { get; set; }
        public int? UserVoteId { get; set; }
        public bool? UserVoteIsSupporting { get; set; }
        public int AbstentionCount { get; set; }
        public List<string> MemberPermissions { get; set; }
        public bool UsersOwnSuggestion { get; set; }
    }
}