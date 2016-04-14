using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
    }
}