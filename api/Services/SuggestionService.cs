using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;
using StoredObjects;
using WebApiResources;

namespace Services
{
    public interface ISuggestionServiceDependencies
    {
        IStorageService StorageService { get; set; }
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }
    }
    public class SuggestionServiceDependencies: ISuggestionServiceDependencies
    {
        public IOrganisationService OrganisationService { get; set; }
        public IUserService UserService { get; set; }
        public IStorageService StorageService { get; set; }

        public SuggestionServiceDependencies(IOrganisationService organisationService,
            IUserService userService,
            IStorageService storageService
            )
        {
            OrganisationService = organisationService;
            UserService = userService;
            StorageService = storageService;
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

            var suggestion = _dependencies.StorageService.SetOf<Suggestion>().Create();
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

            var suggestionsQuery = member.Organisation.Members
                .SelectMany(m => m.Suggestions)
                .Where(x => !x.AuthorMember.Removed);
            if (request.MemberId.HasValue)
            {
                suggestionsQuery = suggestionsQuery.Where(s => s.AuthorMemberId == request.MemberId.Value);
            }
            var suggestions = suggestionsQuery
                .OrderByDescending(s => s.CreatedDateUtc)
                .Skip(((request.Page ?? 1) - 1)*10).Take(10);
            
            return new SearchSugestionsResponse
            {
                OrganisationId = request.OrganisationId,
                OrganisationName = member.Organisation.Name,
                PageCount = (int)Math.Ceiling(suggestionsQuery.Count()/10.0),
                Suggestions = suggestions.Select(BuildSummarySuggestion).ToList()
            };
                
            throw new NotImplementedException();
        }

        public virtual SuggestionSummaryResource BuildSummarySuggestion(Suggestion suggestion)
        {
            return new SuggestionSummaryResource
            {
                Id = suggestion.Id,
                Subject = suggestion.ShortDescription,
                DateTimeText = suggestion.CreatedDateUtc.ToString("s"),
                NetSupportPercent = GetPercentSupport(suggestion),
                VotingPercent = GetVotingPercent(suggestion),
                AuthorMemberId = suggestion.AuthorMemberId,
                AuthorPublicName = suggestion.AuthorMember.PublicName,
                AuthorPictureUrl = suggestion.AuthorMember.MemberAuth0Users.First().Auth0User.PictureUrl
            };
         //   throw new NotImplementedException();
        }

        public double GetVotingPercent(Suggestion suggestion)
        {
            var support = GetVoteCount(suggestion, true);
            var opposition = GetVoteCount(suggestion, false);
            return (100.0 * (support+opposition+GetAbstentionCount(suggestion))) / 
                (suggestion.AuthorMember.Organisation.Members.Where(m=>!m.Removed).Count());
            throw new NotImplementedException();
        }

        public virtual double GetPercentSupport(Suggestion suggestion)
        {
            var support = GetVoteCount(suggestion, true);
            var opposition = GetVoteCount(suggestion, false);
            var abstentions = GetAbstentionCount(suggestion);

            if ((support + opposition+ abstentions) == 0) { return 0; }
            return Math.Round( (100.0 * (support-opposition)) / (support + opposition+ abstentions),1);
            throw new NotImplementedException();
        }

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
            var vote = suggestion.SuggestionVotes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            return new SuggestionDetailResource
            {   
                OrganisationId=suggestion.AuthorMember.OrganisationId,
                Suggestion = suggestion.FullText,
                SuggestionSummary = BuildSummarySuggestion(suggestion),
                UserVoteId = vote?.Id,
                UserVoteIsSupporting = vote?.MemberIsSupportingSuggestion,
                MemberPermissions = _dependencies.OrganisationService.GetMemberPermissions(user,member.Organisation),
                UsersOwnSuggestion = suggestion.AuthorMemberId==member.Id,
                VotesFor = GetVoteCount(suggestion,true),
                VotesAgainst = GetVoteCount(suggestion,false),
                AbstentionCount = GetAbstentionCount(suggestion),
                VoteByLeader = vote?.VoteByLeader
            };

            throw new NotImplementedException();
        }

        public virtual ResponseResource Vote(IPrincipal principal, VoteOnSuggestionsRequest request)
        {
            var suggestion = GetGuaranteedSuggestion(request.SuggestionId);
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, suggestion.AuthorMember.OrganisationId);
            //if (member.Organisation.CountingInProgress)
            //{
            //    return new ResponseResource {Error = "Vote counting is in progress - please try again later",HasError = true};
            //}
            var vote = suggestion.SuggestionVotes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            if (vote == null)
            {
                vote = _dependencies.StorageService.SetOf<SuggestionVote>().Create();
                vote.SuggestionId = request.SuggestionId;
                vote.Suggestion = suggestion;
                vote.VoterMemberId = member.Id;
                vote.VoteByLeader = false;
                vote.VoterMember = member;
                _dependencies.StorageService.SetOf<SuggestionVote>().Add(vote);
            }
            vote.MemberIsSupportingSuggestion = request.VotingInSupport;
            vote.LastUpdateDateTimeUtc=DateTime.UtcNow;
            VoteForFollowers(request.VotingInSupport, suggestion, member);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        private void VoteForFollowers(bool? votingInSupport, Suggestion suggestion, Member member)
        {
            var followers= member.Followers.Select(f=>f.Member).ToList();
            followers.ForEach(f=>VoteForFollower(f,votingInSupport,suggestion));
        }

        private void VoteForFollower(Member member, bool? votingInSupport, Suggestion suggestion)
        {
            var vote = suggestion.SuggestionVotes.SingleOrDefault(v => v.VoterMemberId == member.Id);
            if (vote == null)
            {
                vote = _dependencies.StorageService.SetOf<SuggestionVote>().Create();
                vote.SuggestionId = suggestion.Id;
                vote.Suggestion = suggestion;
                vote.VoterMemberId = member.Id;
                vote.VoterMember = member;
                vote.VoteByLeader = true;
                vote.MemberIsSupportingSuggestion = votingInSupport;
                vote.LastUpdateDateTimeUtc = DateTime.UtcNow;
                _dependencies.StorageService.SetOf<SuggestionVote>().Add(vote);
                VoteForFollowers(votingInSupport, suggestion, member);
            }
            else if(vote.VoteByLeader)
            {
                vote.MemberIsSupportingSuggestion = votingInSupport;
                vote.LastUpdateDateTimeUtc = DateTime.UtcNow;
                VoteForFollowers(votingInSupport, suggestion, member);
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
                _dependencies.StorageService.SetOf<SuggestionVote>().Remove(vote);
                _dependencies.StorageService.SaveChanges();
            }
            else
            {
                return new ResponseResource {HasError = true,Error = "vote not found"};
            }
            return new ResponseResource();
        }

        public virtual int GetVoteCount(Suggestion suggestion,bool inFavour)
        {
            return suggestion.SuggestionVotes.Where(v => 
            !v.VoterMember.Removed
            && v.MemberIsSupportingSuggestion.HasValue 
            && v.MemberIsSupportingSuggestion.Value==inFavour)
                .Sum(v => 1 + v.DelegatedVoteCount);
        }
        public virtual int GetAbstentionCount(Suggestion suggestion)
        {
            return suggestion.SuggestionVotes.Where(
                v => 
                !v.MemberIsSupportingSuggestion.HasValue
                && !v.VoterMember.Removed)
                .Sum(v => 1 + v.DelegatedVoteCount);
        }
    }
}
