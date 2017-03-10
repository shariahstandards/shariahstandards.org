using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class AllOrganisationPermissionsResource
    {
        public List<PermissionResource> CurrentUserPermissions { get; set; }
        public List<PermissionResource> AllPermissionResources { get; set; }
        public List<MemberPermissionListResource> DelegatedPermissions { get; set; }
        public MemberResource Leader { get; set; }
        public bool CanManagePermissions { get; set; }
    }

    public class AddDelegatedPermissionRequest
    {
        public int OrganisationId { get; set; }
        public string PermissionValue { get; set; }
    }
    public class RemoveDelegatedPermissionRequest
    {
        public int DelegatedPermissionId { get; set; }
    }
    public class MemberPermissionListResource
    {
        public string PublicName
        {
            get; set;
        }
        public int MemberId { get; set; }

        public List<PermissionResource> Permissions { get; set; } 
    }

    public class PermissionResource
    {
        public bool IsDelegatedPermission { get; set; }
        public int? DelegatedPermissionId { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
    }
}
