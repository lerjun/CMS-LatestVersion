using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthSystem.Models
{
    public class GlobalModel
    {
        public string Token { get; set; }
        public int PageStatus { get; set; }
        public string Status { get; set; }
        public string PageLink { get; set; }
        public string UserId { get; set; }
    }
}
