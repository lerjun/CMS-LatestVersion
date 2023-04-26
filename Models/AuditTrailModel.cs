using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AuthSystem.Models
{
    public class AuditTrailModel
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Actions { get; set; }

        [Column(TypeName = "Varchar(max)")]
        public string? Module { get; set; }

        [Column(TypeName = "int")]
        public int? UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? DateCreated { get; set; }
    }
}
