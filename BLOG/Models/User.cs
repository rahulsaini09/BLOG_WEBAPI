using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLOG.Models
{
    public class User
    {
        public int Id { get; set; }
        public string LoginId { get; set; }
        public string  UserName { get; set; }
        public string RoleName { get; set; }
        public string UserKey { get; set; }
    }

}
