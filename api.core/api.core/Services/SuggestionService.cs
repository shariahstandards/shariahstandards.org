using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using WebApiResources;

namespace Services
{
    public interface ISuggestionServiceDependencies
    {
        IStorageService StorageService { get; set; }
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }
        IMemberService MemberService { get; set; }
    }
    public class SuggestionServiceDependencies: ISuggestionServiceDependencies
    {
        public IOrganisationService OrganisationService { get; set; }
        public IUserService UserService { get; set; }
        public IStorageService StorageService { get; set; }
        public IMemberService MemberService { get; set; }

        public SuggestionServiceDependencies(IOrganisationService organisationService,
            IUserService userService,
            IStorageService storageService,
            IMemberService memberService
            )
        {
            OrganisationService = organisationService;
            UserService = userService;
            StorageService = storageService;
            MemberService = memberService;
        }
    }
    public interface ISuggestionService
    {
        ResponseResource CreateSuggestion(IPrincipal principal, CreateSugestionRequest request);
        ResponseResource DeleteSuggestion(IPrincipal principal, DeleteSugestionRequest request);
        SearchSugestionsResponse Search(IPrincipal principal, SearchSuggestionsRequest request);
        SuggestionDetailResource ViewSuggestion(IPrincipal user, ViewSugestionRequest request);
        ResponseResource Vote(IPrincipal user, VoteOnSuggestionsRequest request);
        ResponseResource RemoveVote(IPrincipal user, RemoveVoteOnSuggestionsRequest request);
        ResponseResource CommentOnSuggestion(IPrincipal principal, CreateSugestionCommentRequest request);
    }
    public class SuggestionService: ISuggestionService
    {
        private readonly ISuggestionServiceDependencies _dependencies;
        public SuggestionService(ISuggestionServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public ResponseResource CreateSuggestion(IPrincipal principal, CreateSugestionRequest request)
        {
            var organisationId = request.OrganisationId;
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, organisationId);

            var suggestion =new Suggestion();
            suggestion.AuthorMember = member;
            suggestion.AuthorMemberId = member.Id;
            suggestion.FullText = request.Suggestion;
            suggestion.ShortDescription = request.Subject;
            suggestion.CreatedDateUtc = DateTime.UtcNow;

            _dependencies.StorageService.SetOf<Suggestion>().Add(suggestion);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

       

        public SearchSugestionsResponse Search(IPrincipal principal, SearchSuggestionsRequest request)
        {
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, request.OrganisationId);
            var resource = new SearchSugestionsResponse();
            resource.OrganisationId = request.OrganisationId;
            resource.OrganisationName = member.Organisation.Name;
            
            var suggestionsQuery = member.Organisation.Members
                .SelectMany(m => m.Suggestions.Where(s=>!s.Removed && !s.PendingModeration))
                .Where(x => !x.AuthorMember.Removed);
            if (request.MemberId.HasValue)
            {
                var memberSearchedFor = member.Organisation.Members.SingleOrDefault(m => m.Id == request.MemberId.Value);
                if (memberSearchedFor != null)
                {
                    if (memberSearchedFor.Removed)
                    {
                        resource.Suggestions=new List<SuggestionSummaryResource>();
                        return resource;
                    }
                    resource.MemberSearchedFor =
                        _dependencies.MemberService.BuildSearchedMemberResource(memberSearchedFor);
                suggestionsQuery = suggestionsQuery.Where(s => s.AuthorMemberId == request.MemberId.Value);

                }
            }
            if (!request.MostRecentFirst)
            {
                suggestionsQuery =
                    suggestionsQuery.OrderByDescending(
                        s =>
                            s.Votes
                            .Count(
                                x => (x.MemberIsSupportingSuggestion.HasValue && x.MemberIsSupportingSuggestion.Value))
                            -s.Votes
                            .Count(
                                x => (x.MemberIsSupportingSuggestion.HasValue && !x.MemberIsSupportingSuggestion.Value))
                                );
            }
            else
            {
                suggestionsQuery = suggestionsQuery
                    .OrderByDescending(s => s.CreatedDateUtc);
            }
            var suggestions = suggestionsQuery
                .Skip(((request.Page ?? 1) - 1)*10).Take(10);



            resource.PageCount = (int) Math.Ceiling(suggestionsQuery.Count()/10.0);
            resource.Suggestions = suggestions.Select(s=>BuildSummarySuggestion(s,member)).ToList();
            return resource;
        }

        public virtual SuggestionSummaryResource BuildSummarySuggestion(Suggestion suggestion,Member member)
        {
            var vote = suggestion.Votes.SingleOrDefault(v => v.VoterMemberId == member.Id);
                
            var resource = new SuggestionSummaryResource();
            resource.UserVoteId = vote?.Id;
            resource.UserVoteIsSupporting = vote?.MemberIsSupportingSuggestion;
            
            resource.Id = suggestion.Id;
            resource.Subject = suggestion.ShortDescription;
            resource.FullText = suggestion.FullText;
            resource.DateTimeText = suggestion.CreatedDateUtc.ToString("s");
            resource.For = GetVoteCount(suggestion, true);
            resource.Against = GetVoteCount(suggestion, false);
            resource.Abstaining = GetAbstentionCount(suggestion);
            double count = resource.For + resource.Against + resource.Abstaining;
            resource.PercentFor = Math.Round(resource.For*100/count);
            resource.PercentAgainst = Math.Round(resource.Against * 100 / count); ;
            resource.PercentAbstaining = Math.Round(resource.Abstaining * 100 / count); ;
            resource.VotingPercent = GetVotingPercent(suggestion);
            resource.AuthorMemberId = suggestion.AuthorMemberId;
            resource.AuthorPublicName = suggestion.AuthorMember.PublicName;
            resource.AuthorPictureUrl = suggestion.AuthorMember.MemberAuth0Users.First().Auth0User.PictureUrl;
            return resource;
        }

        public double GetVotingPercent(Suggestion suggestion)
        {
            var support = GetVoteCount(suggestion, true);
            var opposition = GetVoteCount(suggestion, false);
            var percent= (100.0 * (support+opposition+GetAbstentionCount(suggestion))) / 
                (suggestion.AuthorMember.Organisation.Members.Count(m => !m.Removed));

            return Math.Round(percent, 3);
        }

        //public virtual double GetPercentSupport(Suggestion suggestion)
        //{
        //    var support = GetVoteCount(suggestion, true);
        //    var opposition = GetVoteCount(suggestion, false);
        //    var abstentions = GetAbstentionCount(suggestion);

        //    if ((support + opposition+ abstentions) == 0) { return 0; }
        //    return Math.Round( (100.0 * (support-opposition)) / (support + opposition+ abstentions),1);
        //    throw new NotImplementedException();
        //}

        public ResponseResource DeleteSuggestion(IPrincipal principal, DeleteSugestionRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var suggestion = GetGuaranteedSuggestion(request.SuggestionId);
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, suggestion.AuthorMember.OrganisationId);
            if(suggestion.AuthorMemberId != member.Id)
            {
                _dependencies.OrganisationService.GuaranteeUserHasPermission(user, member.Organisation, ShurahOrganisationPermission.RemoveSuggestion);
            }
            _dependencies.StorageService.SetOf<Suggestion>().Remove(suggestion);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        private Suggestion GetGuaranteedSuggestion(int suggestionId)
        {
            var suggestion = _dependencies.StorageService.SetOf<Suggestion>().SingleOrDefault(s => s.Id == suggestionId);
            if (suggestion == null)
            {
                throw new Exception("Suggestion not found");
            }
            return suggestion;
        }

        public virtual SuggestionDetailResource ViewSuggestion(IPrincipal principal, ViewSugestionRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var suggestion = _dependencies.StorageService.SetOf<Suggestion>().FirstOrDefault(s =>
                s.AuthorMember.Organisation.Members.Any(m => m.MemberAuth0Users.Any(a => a.Auth0UserId == user.Id))
                && s.Id == request.SuggestionId);
            if (suggestion == null)
            {
                return new SuggestionDetailResource { HasError = true, Error = "Suggestion not found." };
            }
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, suggestion.AuthorMember.OrganisationId);
            var vote = suggestion.Votes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            return new SuggestionDetailResource
            {   
                OrganisationId=suggestion.AuthorMember.OrganisationId,
                Suggestion = suggestion.FullText,
                SuggestionSummary = BuildSummarySuggestion(suggestion,member),
                UserVoteId = vote?.Id,
                UserVoteIsSupporting = vote?.MemberIsSupportingSuggestion,
                MemberPermissions = _dependencies.OrganisationService.GetMemberPermissions(user,member.Organisation),
                UsersOwnSuggestion = suggestion.AuthorMemberId==member.Id,
                VotesFor = GetVoteCount(suggestion,true),
                VotesAgainst = GetVoteCount(suggestion,false),
                AbstentionCount = GetAbstentionCount(suggestion),
                VoteByLeader = vote?.VotingLeaderMemberId.HasValue,
                Comments = suggestion.Comments.Where(c=>!c.IsCensored || c.CommentingMemberId==member.Id).Select(BuildCommentResource).ToList()
            };
        }

        public virtual SuggestionCommentResource BuildCommentResource(SuggestionComment comment)
        {
            var resource = new SuggestionCommentResource();
            resource.CommentId = comment.Id;
            resource.Comment = comment.Comment;
            resource.DateTimeText = comment.LastUpdateDateTimeUtc.ToString("s");
            resource.PublicNameOfCommentAuthor = comment.CommentingMember.PublicName;
            resource.IsSupportive = comment.CommentIsSupportingSuggestion;
            resource.Censored = comment.IsCensored;
            resource.PictureUrl = comment.CommentingMember.MemberAuth0Users.First().Auth0User.PictureUrl;
            return resource;
        }

        public virtual ResponseResource Vote(IPrincipal principal, VoteOnSuggestionsRequest request)
        {
            var suggestion = GetGuaranteedSuggestion(request.SuggestionId);
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, suggestion.AuthorMember.OrganisationId);
            //if (member.Organisation.CountingInProgress)
            //{
            //    return new ResponseResource {Error = "Vote counting is in progress - please try again later",HasError = true};
            //}
            var vote = suggestion.Votes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            if (vote == null)
            {
                vote = new SuggestionVote();
                vote.SuggestionId = request.SuggestionId;
                vote.Suggestion = suggestion;
                vote.VoterMemberId = member.Id;
                vote.VotingLeaderMemberId = null;
                vote.VoterMember = member;
                _dependencies.StorageService.SetOf<SuggestionVote>().Add(vote);
            }
            vote.MemberIsSupportingSuggestion = request.VotingInSupport;
            vote.LastUpdateDateTimeUtc=DateTime.UtcNow;
            VoteForFollowers(request.VotingInSupport, suggestion, member,member);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        private void VoteForFollowers(bool? votingInSupport, Suggestion suggestion, Member member, Member leader)
        {
            var followers= member.Followers.Select(f=>f.Member).ToList();
            followers.ForEach(f=>VoteForFollower(f,votingInSupport,suggestion,leader));
        }

        private void VoteForFollower(Member member, bool? votingInSupport, Suggestion suggestion, Member leader)
        {
            var vote = suggestion.Votes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            if (vote == null)
            {
                vote = new SuggestionVote();
                vote.SuggestionId = suggestion.Id;
                vote.Suggestion = suggestion;
                vote.VoterMemberId = member.Id;
                vote.VoterMember = member;
                vote.VotingLeaderMemberId = leader.Id;
                vote.VotingLeaderMember = leader;
                vote.MemberIsSupportingSuggestion = votingInSupport;
                vote.LastUpdateDateTimeUtc = DateTime.UtcNow;
                _dependencies.StorageService.SetOf<SuggestionVote>().Add(vote);
                VoteForFollowers(votingInSupport, suggestion, member,leader);
            }
            else if(vote.VotingLeaderMemberId==leader.Id)
            {
                vote.MemberIsSupportingSuggestion = votingInSupport;
                vote.LastUpdateDateTimeUtc = DateTime.UtcNow;
                VoteForFollowers(votingInSupport, suggestion, member,leader);
            }

        }

        public ResponseResource RemoveVote(IPrincipal principal, RemoveVoteOnSuggestionsRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var vote =
                user.MemberAuth0Users.SelectMany(m => m.Member.SuggestionVotes)
                    .SingleOrDefault(x => x.Id == request.VoteId);
            if (vote != null)
            {
                var member = _dependencies.OrganisationService.GetGuaranteedMember(principal,
                    vote.Suggestion.AuthorMember.OrganisationId);
                var followerVotes =
                    member.SuggestionFollowerVotes.Where(v => v.SuggestionId == vote.SuggestionId).ToList();
                _dependencies.StorageService.SetOf<SuggestionVote>().Remove(vote);
                followerVotes.ForEach(v=>_dependencies.StorageService.SetOf<SuggestionVote>().Remove(v));
                _dependencies.StorageService.SaveChanges();
            }
            else
            {
                return new ResponseResource {HasError = true,Error = "vote not found"};
            }
            return new ResponseResource();
        }

        public ResponseResource CommentOnSuggestion(IPrincipal principal, CreateSugestionCommentRequest request)
        {
            var suggestion = GetGuaranteedSuggestion(request.SuggestionId);
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, suggestion.AuthorMember.OrganisationId);
            var comment = new SuggestionComment();
            comment.CommentingMemberId = member.Id;
            comment.Comment = request.Comment;
            comment.CommentIsSupportingSuggestion = request.Supporting;
            comment.CommentingMember = member;
            comment.LastUpdateDateTimeUtc = DateTime.UtcNow;
            comment.Suggestion = suggestion;
            comment.IsCensored = member.Moderated;
            comment.SuggestionId = suggestion.Id;
            _dependencies.StorageService.SetOf<SuggestionComment>().Add(comment);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public virtual int GetVoteCount(Suggestion suggestion,bool inFavour)
        {
            return suggestion.Votes
                .Count(v => !v.VoterMember.Removed && v.MemberIsSupportingSuggestion.HasValue 
                && v.MemberIsSupportingSuggestion.Value==inFavour);
        }
        public virtual int GetAbstentionCount(Suggestion suggestion)
        {
            return suggestion.Votes
                .Count(v => !v.MemberIsSupportingSuggestion.HasValue
                && !v.VoterMember.Removed);
        }
    }
}
