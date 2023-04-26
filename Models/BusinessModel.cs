using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class BusinessModel
    {
  [Key]
        public int Id { get; set; }

        public string? BusinessName { get; set; }

        public int? TypeId { get; set; } 
        public int? LocationID { get; set; }  
        public string? Description { get; set; }
        public string? Address { get; set; }

        public string? Cno { get; set; }

        public string? Email { get; set; }
        public string? Url { get; set; }
        public string? Services { get; set; }

        public string? FeatureImg { get; set; }

        public string? Gallery { get; set; }

        public int? Active { get; set; }
        public string? FilePath { get; set; }
        public string? Map { get; set; }



    }
}
