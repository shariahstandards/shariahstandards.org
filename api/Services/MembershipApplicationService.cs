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
    public interface IMembershipApplicationServiceDependencies
    {
        IStorageService StorageService { get; set; }
        IUserService UserService { get; set; }
        IOrganisationService OrganisationService { get; set; }

    }
    public class MembershipApplicationServiceDependencies : IMembershipApplicationServiceDependencies
    {
        public IStorageService StorageService { get; set; }
        public IUserService UserService { get; set; }
        public IOrganisationService OrganisationService { get; set; }

        public MembershipApplicationServiceDependencies(IStorageService storageService,
            IUserService userService,IOrganisationService organisationService
            )
        {
            StorageService = storageService;
            UserService = userService;
            OrganisationService = organisationService;
        }
    }
    public interface IMembershipApplicationService
    {
        ResponseResource ApplyToJoin(IPrincipal principal, MembershipApplicationrequest request);
        MembershipApplicationSearchResultsResource SearchMembershipApplications(IPrincipal principle, MembershipApplicationSearchRequest request);
        ResponseResource AcceptMembershipApplication(IPrincipal principal, MembershipApplicationAcceptanceRequest request);
        ResponseResource RejectMembershipApplication(IPrincipal principal, MembershipApplicationRejectionRequest request);
    }

    public class MembershipApplicationService : IMembershipApplicationService
    {
        private readonly IMembershipApplicationServiceDependencies _dependencies;

        public MembershipApplicationService(IMembershipApplicationServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual ResponseResource ApplyToJoin(IPrincipal principal, MembershipApplicationrequest request)
        {
            if (!request.AgreesToTermsAndConditions)
            {
              return new ResponseResource { HasError = true, Error = "You must agree to the terms and conditions" };
            }
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);

            var existingMemberWithSameEmailAddress = _dependencies.StorageService.SetOf<Member>()
                .FirstOrDefault(m => m.OrganisationId == request.OrganisationId
                                    && !m.Removed
                                     && m.MemberAuth0Users.Any(a => a.Auth0UserId != user.Id)
                                     && m.EmailAddress == request.EmailAddress);

            if (existingMemberWithSameEmailAddress != null)
            {
                return new ResponseResource {HasError = true, Error = "Email Address already used by another member"};
            }

            var existingMemberWithSamePublicName = _dependencies.StorageService.SetOf<Member>()
                .FirstOrDefault(x => x.OrganisationId == request.OrganisationId
                                     && x.MemberAuth0Users.Any(m => m.Auth0UserId != user.Id)
                                     && x.PublicName == request.PublicName);

            if (existingMemberWithSamePublicName != null)
            {
                return new ResponseResource {HasError = true, Error = "Public name already used by another member"};
            }

            var existingApplication =
                user.MembershipApplications.SingleOrDefault(a => a.OrganisationId == request.OrganisationId);
            if (existingApplication == null)
            {
                existingApplication = _dependencies.StorageService.SetOf<MembershipApplication>().Create();
                existingApplication.Auth0User = user;
                existingApplication.Auth0UserId = user.Id;
                existingApplication.OrganisationId = request.OrganisationId;
                _dependencies.StorageService.SetOf<MembershipApplication>().Add(existingApplication);

            }
            existingApplication.Email = request.EmailAddress;
            existingApplication.SupportingStatement = request.PublicProfileStatement;
            existingApplication.PhoneNumber = request.PhoneNumber;
            existingApplication.PublicName = request.PublicName;
            existingApplication.DateAppliedUtc=DateTime.UtcNow;
            _dependencies.StorageService.SaveChanges();

            //TODO send email / sms to organisation leader / members with application approval rights

            return new ResponseResource();

        }

        public MembershipApplicationSearchResultsResource SearchMembershipApplications(IPrincipal principle,
            MembershipApplicationSearchRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principle);
            var organisation = _dependencies.OrganisationService.GetOrganisation(request.OrganisationId);
            _dependencies.OrganisationService.GuaranteeUserHasPermission(user, organisation,
                ShurahOrganisationPermission.AcceptMembershipApplication);

            return new MembershipApplicationSearchResultsResource
            {
                OrganisationId = request.OrganisationId,
                Results = organisation.MembershipApplications.Where(
                    x => x.Acceptances.Count(a => !a.AcceptingMember.Removed)
                         < organisation.RequiredNumbersOfAcceptingMembers)
                    .OrderBy(x=>x.DateAppliedUtc)
                    .Skip((request.Page-1)*10).Take(10).ToList()
                    .Select(BuildMembershipApplicationResource)
                    .ToList()
            };

        }

        public virtual ResponseResource AcceptMembershipApplication(IPrincipal principal, MembershipApplicationAcceptanceRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var application =
                _dependencies.StorageService.SetOf<MembershipApplication>().SingleOrDefault(x => x.Id == request.Id);
            if (application == null)
            {
                return new ResponseResource {HasError = true,Error = "Application not found."};
            }
            _dependencies.OrganisationService.GuaranteeUserHasPermission(user,application.Organisation,ShurahOrganisationPermission.AcceptMembershipApplication);
            var currentMember = _dependencies.OrganisationService.GetGuaranteedMember(principal, application.OrganisationId);
            var existingAcceptance = application.Acceptances.SingleOrDefault(x => x.AcceptingMemberId == currentMember.Id);
            if (existingAcceptance != null)
            {
                return new ResponseResource {HasError = true,Error = "You have already accepted this application."};
            }
            var acceptance = _dependencies.StorageService.SetOf<MembershipApplicationAcceptance>().Create();
            acceptance.AcceptingMemberId = currentMember.Id;
            acceptance.AcceptanceDateTimeUtc=DateTime.UtcNow;
            acceptance.AcceptingMember = currentMember;
            acceptance.MembershipApplication = application;
            acceptance.MembershipApplicationId = application.Id;
            _dependencies.StorageService.SetOf<MembershipApplicationAcceptance>().Add(acceptance);
            _dependencies.StorageService.SaveChanges();

            var acceptancesCount = application.Acceptances.Count(x => !x.AcceptingMember.Removed);
            if (acceptancesCount >= application.Organisation.RequiredNumbersOfAcceptingMembers)
            {
                var existingMembership = application.Organisation.Members.FirstOrDefault(
                    m => m.MemberAuth0Users.Any(a => a.Auth0UserId == application.Auth0UserId));
                if (existingMembership == null)
                {
                    existingMembership = _dependencies.StorageService.SetOf<Member>().Create();
                    _dependencies.StorageService.SetOf<Member>().Add(existingMembership);

                }
                existingMembership.OrganisationId = application.OrganisationId;
                existingMembership.PublicName = application.PublicName;
                existingMembership.EmailAddress = application.Email;
                existingMembership.Introduction = "";
                existingMembership.JoinedOnDateAndTimeUtc = DateTime.UtcNow;
                existingMembership.LastDateAndTimeUtcAgreedToMembershipRules = DateTime.UtcNow;
                existingMembership.Organisation = application.Organisation;
                existingMembership.Removed = false;
                _dependencies.StorageService.SaveChanges();

                var existingMemberAuth0User =
                    _dependencies.StorageService.SetOf<MemberAuth0User>()
                        .SingleOrDefault(
                            x =>
                                x.Auth0UserId == acceptance.MembershipApplication.Auth0UserId &&
                                x.MemberId == existingMembership.Id);
                if (existingMemberAuth0User == null)
                {
                    existingMemberAuth0User = _dependencies.StorageService.SetOf<MemberAuth0User>().Create();
                    existingMemberAuth0User.Auth0UserId = acceptance.MembershipApplication.Auth0UserId;
                    existingMemberAuth0User.Auth0User = acceptance.MembershipApplication.Auth0User;
                    existingMemberAuth0User.MemberId = existingMembership.Id;
                    existingMemberAuth0User.Member = existingMembership;
                    _dependencies.StorageService.SetOf<MemberAuth0User>().Add(existingMemberAuth0User);
                }
                existingMemberAuth0User.Suspended = false;
                _dependencies.StorageService.SaveChanges();
            }

            return new ResponseResource();
        }

        public virtual ResponseResource RejectMembershipApplication(IPrincipal principal, MembershipApplicationRejectionRequest request)
        {
            var user = _dependencies.UserService.GetGuaranteedAuthenticatedUser(principal);
            var application =
                _dependencies.StorageService.SetOf<MembershipApplication>().SingleOrDefault(x => x.Id == request.Id);
            if (application == null)
            {
                return new ResponseResource { HasError = true, Error = "Application not found." };
            }
            _dependencies.OrganisationService.GuaranteeUserHasPermission(user, application.Organisation, ShurahOrganisationPermission.AcceptMembershipApplication);
            var currentMember = _dependencies.OrganisationService.GetGuaranteedMember(principal, application.OrganisationId);
            var existingAcceptance = application.Acceptances.SingleOrDefault(x => x.AcceptingMemberId == currentMember.Id);
            if (existingAcceptance != null)
            {
                return new ResponseResource { HasError = true, Error = "You have already accepted this application." };
            }
            //TODO send email with rejection reason
            _dependencies.StorageService.SetOf<MembershipApplication>().Remove(application);
            _dependencies.StorageService.SaveChanges();
            return new ResponseResource();
        }

        public virtual MembershipApplicationResource BuildMembershipApplicationResource(MembershipApplication application)
        {
            return new MembershipApplicationResource
            {
                Id=application.Id,
                PublicName = application.PublicName,
                EmailAddress = application.Email,
                PhoneNumber=application.PhoneNumber,
                SupportingStatement = application.SupportingStatement,
                PictureUrl = application.Auth0User.PictureUrl,
            };
        }
    }

    public interface IUrlSlugServiceDependencies
    {
    }
    public class UrlSlugServiceDependencies : IUrlSlugServiceDependencies
    {
    }

    public interface IUrlSlugService
    {
        string GetSlug(string text);
    }
    public class UrlSlugService : IUrlSlugService
    {
        private readonly IUrlSlugServiceDependencies _dependencies;

        public UrlSlugService(IUrlSlugServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual string GetSlug(string text)
        {
            var result = text.Replace("'", "");
            result = result.Replace(" ", "-");
            result = result.Replace("&", "-and-");
            result = result.Replace("--", "-");
            result = result.ToLower();
            result = Uri.EscapeUriString(result);
            var parts = result.Split('%').ToList();
            result = parts.First() + string.Concat(parts.Skip(1).Select(p => p.Substring(2)));
            result = result.Trim('-');
            return result;

        }
    }
}
