using Microsoft.AspNetCore.Identity;

namespace Agri_Energy_Connect.Models
{
    public class User : IdentityUser
    {
        public int? FarmerId { get; set; }
        public Farmer Farmer { get; set; }

        public int? EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
