using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class CorporateModelVM
    {
        public int Id { get; set; }

        public string? CorporateName { get; set; }

        public string? CompanyID { get; set; }

        public string? Address { get; set; }

        public string? CNo { get; set; }

        public string? EmailAddress { get; set; }

        public string? Status { get; set; }

        public string? DateCreated { get; set; }

        public string? UserCount { get; set; }

        public string? VIPCount { get; set; }

        public string? Tier { get; set; }

        public string? Description { get; set; }

        public string? MembershipID { get; set; }
        public string? memid { get; set; }
        public int? Status_ID { get; set; }


    }
}
