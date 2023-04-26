using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthSystem.ViewModel
{
    public class AdminUserVM 
    {
        public int Id { get; set; }

        public string Username { get; set; }


        public string Fname { get; set; }

  
        public string Lname { get; set; }


        public string Email { get; set; }

 
        public string Gender { get; set; }

        public string EmployeeID { get; set; }

        public string Position { get; set; }
        public string PositionID { get; set; }

        public string Corporatename { get; set; }
        public string CorporateID { get; set; }

        public string UserType { get; set; }

        public string Fullname { get; set; }
        public string DateCreated { get; set; }
              public string FilePath { get; set; }
        public string isVIP { get; set; }
        public string status { get; set; }
    }
}
