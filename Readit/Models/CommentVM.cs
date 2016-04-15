using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Readit.Models
{
    public class CommentVM
    {
        public int CommentId { get; set; }
        public string Author { get; set; }

        public string Body { get; set; }

        public double TimeSinceCreation { get; set; }

        public int Score { get; set; }
    }
}