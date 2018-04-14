using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects.dialog
{
  public class DecisionTree
  {
    public ShurahBasedOrganisation Organisation { get; set; }
     
  }
  public class Declaration
  {
    public int Id { get; set; }
    public string Question { get; set; }
    public Attribution YesAttribution { get; set; }
    public Attribution NoAttribution { get; set; }
    public Attribution UnsureAttribution { get; set; }
    public IList<AttributionRequirement> Prerequisites { get; set; }
  }
  public class AttributionRequirement
  {
    public Attribution Attribution { get; set; }
    public int ReferralsNeeded { get; set; }
    public IList<AttributionRequirement> RefereePrerequisites { get; set; }
  }
  public class AttributionCategory
  {
    public string Name { get; set; }
  }
  public class Attribution
  {
    public AttributionCategory Category { get; set; }
    public string Name { get; set; }
    bool RequireValue { get; set; }
  }
  public class SpecificMemberAtribution
  {
    public Member Member { get; set; }
    public virtual Attribution Attribution { get; set; }
    public string Value { get; set; }
  }
  public class MemberAtribution
  {
    public Member Member { get; set; }
    public virtual Attribution Attribution { get; set; }
  }
  public class ReferredMemberAtribution
  {
    public Member Member { get; set; }
    public Member ReferringMember { get; set; }
    public virtual Attribution Attribution { get; set; }
  }
}
