using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class UserEvent
    {
        public Guid GuId { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string JsonOfRequest { get; set; }
        public string Auth0UserId { get; set; }
    }
    public class UserEventResponse
    {
        public Guid UserEventGuId { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string JsonOfResponse { get; set; }
    }
}
