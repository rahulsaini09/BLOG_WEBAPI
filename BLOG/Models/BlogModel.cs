using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG.Models
{
    public class BlogModel
    {
        public int Id { get; set;}
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public int createdBy { get; set; }
        public string PublishBy { get; set; }
        public DateTime createdOn { get; set; }
        public string PublishOn => createdOn.ToString("dd-MMM-yyyy");

    }
}
