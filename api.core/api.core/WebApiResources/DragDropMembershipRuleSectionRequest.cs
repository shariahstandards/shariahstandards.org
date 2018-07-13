using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class DragDropMembershipRuleSectionRequest
    {
        public int DraggedMembershipRuleSectionId { get; set; }
        public int DroppedOnMembershipRuleSectionId { get; set; }
    }
    public class DeleteMembershipRuleSectionRequest
    {
        public int MembershipRuleSectionId { get; set; }
    }
}
