using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public class JsonDocument
    {
        public int Id { get; set; }
        public string CollectionName { get; set; }
        public string Document { get; set; }
    }
}
