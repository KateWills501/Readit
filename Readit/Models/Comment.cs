using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Readit.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [MaxLength(20000)]
        public string Body { get; set; }
        [Required]
        public Post Post { get; set; }
        public int UpCount { get; set; }
        public int DownCount { get; set; }
    }
}