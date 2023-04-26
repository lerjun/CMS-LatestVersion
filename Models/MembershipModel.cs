using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class MembershipModel
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Name { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string Description { get; set; }


        public DateTime? DateUsed { get; set; }


        public DateTime? DateEnded { get; set; }

        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "int")]
        public int? Status { get; set; }
    }
}
