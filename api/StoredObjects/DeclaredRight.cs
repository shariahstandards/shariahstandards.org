using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public enum RecognisedRightCategory
    {
        Religion=0,
        FamilyRelationship=1,
        Debt=2,
        Protection=3,
        Zakat=4,
        Bequest=5
    }

    public class RecognisedRight
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RecognisedRightCategory RecognisedRightCategory { get; set; }
        public string WhoHasTheRight { get; set; }
        public string Description { get; set; }
    }
}
