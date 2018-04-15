using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public class DelegatedPermission
    {
        public int Id { get; set; }
        public ShurahOrganisationPermission ShurahOrganisationPermission { get; set; }
        public virtual Member Member { get; set; }
        public int MemberId { get; set; }
    }
}
