using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AuthSystem.Models
{
    public class AppSettings
    {
        public string Key { get; set; }
        public string AdminId { get; set; }
    }
}
