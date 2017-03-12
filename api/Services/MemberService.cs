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
    }
    public class MemberServiceDependencies : IMemberServiceDependencies
    {
        public IUserService UserService { get; set; }
        public IOrganisationService OrganisationService { get; set; }

        public MemberServiceDependencies(IUserService userService,
            IOrganisationService organisationService)
        {
            UserService = userService;
            OrganisationService = organisationService;
        }
    }
    public interface IMemberService
    {
        SearchMemberResponse SearchForMembers(IPrincipal user, SearchMemberRequest request);
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
            var members = member.Organisation.Members;
            response.OrganisationId = request.OrganisationId;
            response.OrganisationName = member.Organisation.Name;
            response.PageCount = members.Count/10;
            response.Members = members.Skip(((request.Page ?? 1) - 1)*10).Take(10).Select(BuildSearchedMemberResource).ToList();
            return response;
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
