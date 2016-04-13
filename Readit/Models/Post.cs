using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Readit.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(10000)]
        public string Body { get; set; }
        //[RegularExpression()]
        public string ExternalLink { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public string Author { get; set; }
    }
}