using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
  public class BatchJobResultResource
  {
    public List<OrganisationUpdateResource> OrganisationUpdates { get; set; }
  }
  public class OrganisationUpdateResource
  {
    public int OrganisationId { get; set; }
    public string Name { get; set; }
    public bool NewLeader { get; set; }
    public string LeaderPublicName { get; set; }
    public int LeaderMemberId { get; set; }
    public int LeaderFollowerCount { get; set; }
  }
}
