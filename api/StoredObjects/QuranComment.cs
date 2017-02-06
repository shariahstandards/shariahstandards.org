using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoredObjects
{
    public class QuranComment
    {
        public int Id { get; set; }
        public int Surah { get; set; }
        public int Verse { get; set; }
        public int Word { get; set; }
        public string CommentText { get; set; }
        public bool Published { get; set; }
        public string Auth0UserId { get; set; }
        public virtual Auth0User Auth0User { get; set; }
        public virtual IList<QuranCommentLink> CommentLinks { get; set; } 
    }
    public class QuranCommentLink
    {
        public int Id { get; set; }
        public int QuranCommentId { get; set; }
        public virtual QuranComment QuranComment { get; set; }
        public int Surah { get; set; }
        public int Verse { get; set; }
        public int Word { get; set; }
    }
}
