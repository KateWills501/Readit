using System.Text.RegularExpressions;

namespace Readit.Models
{
    public class PostVM
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ExternalLink { get; set; }
        public double TimeSinceCreation { get; set; }
        public string Author { get; set; }
        public int Score { get; set; }
        public int NumOfComments { get; set; }
        public bool IsImage
        {
            get
            {
                if (ExternalLink == null) return false;

                var imgRegex = new Regex("(jpg|png|gif|bmp)$");
                if (imgRegex.IsMatch(ExternalLink))
                {
                    return true;
                }
                return false;
            }
        }

        public int Rank { get; set; }
    }
    public class PostDetailVM
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string ExternalLink { get; set; }
        public double TimeSinceCreation { get; set; }
        public string Author { get; set; }
        public bool IsImage
        {
            get
            {
                if (ExternalLink == null) return false;

                var imgRegex = new Regex("(jpg|png|gif|bmp)$");
                if (imgRegex.IsMatch(ExternalLink))
                {
                    return true;
                }
                return false;
            }
        }

        public string Body { get; set; }
    }

}