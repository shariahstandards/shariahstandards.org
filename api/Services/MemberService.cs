using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using StoredObjects;
using WebApiResources;

namespace Services
{
    public interface IMemberServiceDependencies
    {
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }
        IStorageService StorageService { get; set; }
    }
    public class MemberServiceDependencies : IMemberServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IOrganisationService OrganisationService { get; set; }
        public IStorageService StorageService { get; set; }

        public MemberServiceDependencies(IUserService userService,
            IOrganisationService organisationService, IStorageService storageService)
        {
            UserService = userService;
            OrganisationService = organisationService;
            StorageService = storageService;
        }
    }
    public interface IMemberService
    {
        SearchedMemberResource BuildSearchedMemberResource(Member member);
        SearchMemberResponse SearchForMembers(IPrincipal user, SearchMemberRequest request);
        MemberDetailsResource GetMemberDetails(IPrincipal principal, int memberId);
        ResponseResource FollowMember(IPrincipal principal, int leaderMemberId);
        ResponseResource StopFollowingAMember(IPrincipal principal, int organisationId);
    }
    public class MemberService: IMemberService
    {
        private readonly IMemberServiceDependencies _dependencies;

        public MemberService(IMemberServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public SearchMemberResponse SearchForMembers(IPrincipal principal, SearchMemberRequest request)
        {
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, request.OrganisationId);
            var response = new SearchMemberResponse();
            var members = member.Organisation.Members.Where(m=>!m.Removed);
            response.OrganisationId = request.OrganisationId;
            response.OrganisationName = member.Organisation.Name;
            response.PageCount = (int)Math.Ceiling(members.Count()/10.0);
            response.Members = members.Skip(((request.Page ?? 1) - 1)*10).Take(10).Select(BuildSearchedMemberResource).ToList();
            return response;
        }

        public MemberDetailsResource GetMemberDetails(IPrincipal principal, int memberId)
        {
            var member = _dependencies.StorageService.SetOf<Member>().SingleOrDefault(m => m.Id == memberId && !m.Removed);
            if (member == null)
            {
                return new MemberDetailsResource {HasError = true, Error = "Member not found"};
            }
            var currentUserMember = _dependencies.OrganisationService.GetGuaranteedMember(principal,
                member.OrganisationId);
            return BuildMemberDetailsResource(member, currentUserMember);

        }

        public ResponseResource FollowMember(IPrincipal principal, int leaderMemberId)
        {
            var leader = _dependencies.StorageService.SetOf<Member>().SingleOrDefault(m => m.Id == leaderMemberId && !m.Removed);
            if (leader == null)
            {
                return new MemberDetailsResource { HasError = true, Error = "Member not found" };
            }
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, leader.OrganisationId);
            if (member.LeaderRecognition?.RecognisedLeaderMemberId == leaderMemberId)
            {
                return new ResponseResource {HasError = true,Error = "You are already following this member"};
            }
            if (member.Id == leader.Id)
            {
                return new ResponseResource {HasError = true,Error = "You cannot follow yourself"};
            }
            if (IsBeingFollowedByLeader(member, leader))
            {
                return new ResponseResource {HasError = true,Error = "You cannot follow somone who is already following you"};
            }
            if (member.Organisation.CountingInProgress)
            {
                return new ResponseResource {HasError = true,Error = "Counting of leadership votes is in progress - try again later"};
            }
            if (member.LeaderRecognition == null)
            {
                member.LeaderRecognition = _dependencies.StorageService.SetOf<LeaderRecognition>().Create();
                member.LeaderRecognition.Member = member;
                member.LeaderRecognition.MemberId = member.Id;
                _dependencies.StorageService.SetOf<LeaderRecognition>().Add(member.LeaderRecognition);
            }
            member.LeaderRecognition.RecognisedLeaderMember = leader;
            member.LeaderRecognition.RecognisedLeaderMemberId = leaderMemberId;
            member.LeaderRecognition.LastUpdateDateTimeUtc=DateTime.UtcNow;
            _dependencies.StorageService.SaveChanges();

            return new ResponseResource();

        }

        public ResponseResource StopFollowingAMember(IPrincipal principal, int organisationId)
        {
            var member = _dependencies.OrganisationService.GetGuaranteedMember(principal, organisationId);
            if (member.Organisation.CountingInProgress)
            {
                return new ResponseResource {HasError = true,Error = "Leader count in progress. Please try again later"};
            }
            if (member.LeaderRecognition != null)
            {
                _dependencies.StorageService.SetOf<LeaderRecognition>().Remove(member.LeaderRecognition);
                _dependencies.StorageService.SaveChanges();
            }
            return new ResponseResource();
        }

        private bool IsBeingFollowedByLeader(Member member, Member leader)
        {
            var leaderOfLeader = leader.LeaderRecognition?.RecognisedLeaderMember;
            while (leaderOfLeader != null)
            {
                if (leaderOfLeader.Id == member.Id)
                {
                    return true;
                }
                leaderOfLeader = leaderOfLeader.LeaderRecognition?.RecognisedLeaderMember;
            }
            return false;
        }

        public virtual MemberDetailsResource BuildMemberDetailsResource(Member member, Member currentUserMember)
        {
            var resource = new MemberDetailsResource();

            resource.MemberId = member.Id;
            resource.PublicName = member.PublicName;
            resource.PictureUrl = member.MemberAuth0Users.First().Auth0User.PictureUrl;
            resource.DelegatedVotesCount = member.FollowerCount;
            if (currentUserMember.Id == member.Id)
            {
                resource.PrivateMemberDetails = BuildPrivateMemberDetailsResource(member);
            }
            resource.IsFollowedByCurrentUser = currentUserMember.LeaderRecognition?.RecognisedLeaderMemberId ==
                                               member.Id;
            return resource;
        }

        public PrivateMemberDetailsResource BuildPrivateMemberDetailsResource(Member member)
        {
            var resource = new PrivateMemberDetailsResource();
            if (member.LeaderRecognition != null)
            {
                resource.Leader = BuildSearchedMemberResource(member.LeaderRecognition.RecognisedLeaderMember);
            }
            resource.SendNoEmailNotifications = member.SendNoEmailNotifications;
            return resource;
        }

        public virtual SearchedMemberResource BuildSearchedMemberResource(Member member)
        {
            return new SearchedMemberResource
            {
                Id = member.Id,
                PublicName = member.PublicName,
                PictureUrl = member.MemberAuth0Users.First().Auth0User.PictureUrl
            };
        }
    }
}
