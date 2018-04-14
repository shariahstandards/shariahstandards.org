using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiResources
{
    public class SuggestionCommentResource
    {
        public int CommentId;
        public string Comment { get; set; }
        public string DateTimeText { get; set; }
        public string PublicNameOfCommentAuthor { get; set; }
        public bool? IsSupportive { get; set; }
        public bool Censored { get; set; }
    public string PictureUrl { get; set; }
  }
}
