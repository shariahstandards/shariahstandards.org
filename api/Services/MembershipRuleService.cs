using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebApiResources;

namespace Services
{
    public interface IMembershipRuleServiceDependencies
    {
    }
    public class MembershipRuleServiceDependencies : IMembershipRuleServiceDependencies
    {
    }
    public interface IMembershipRuleService
    {
        CreateMembershipRuleRequestResource GetCreateRuleRequestResource(IPrincipal user,int? parentOrganisationId);
    }
    public class MembershipRuleService: IMembershipRuleService
    {
        private readonly IMembershipRuleServiceDependencies _dependencies;

        public MembershipRuleService(IMembershipRuleServiceDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public virtual CreateMembershipRuleRequestResource GetCreateRuleRequestResource(IPrincipal user, int? parentOrganisationId)
        {
            throw new NotImplementedException();
        }
    }
}
