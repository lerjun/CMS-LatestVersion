using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class MembershipModelVM
    {

        public string Id { get; set; }
        public string MembershipName { get; set; }

        public string Description { get; set; }
        public string? DateStarted { get; set; }
        public string? MembershipID { get; set; }
        public int? UserCount { get; set; }
        public int? VIPCount { get; set; }


        public string? DateEnded { get; set; }

        public string? Status { get; set; }



    }
}
