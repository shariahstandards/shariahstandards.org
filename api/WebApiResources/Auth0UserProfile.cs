using System.Collections.Generic;

namespace WebApiResources
{
    public class Auth0UserProfile
    {
        public string Name { get; set; }
        public string Picture { get; set; }
        public string user_id { get; set; }
        public List<Auth0Identity> Identities { get; set; } 
    }

    public class Auth0Identity
    {
        public string provider { get; set; }
        public string user_id { get; set; }
        public string connection { get; set; }
        public bool isLocal { get; set; }

    }
}