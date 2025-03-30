using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Odev_1.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string IdentityNumber { get; set; } = string.Empty;

        [Required]
        public string Country { get; set; } = string.Empty;

        public DateTime? BirthDate { get; set; }

        public bool IsAdmin { get; set; } = false;  // ✅ Admin olup olmadığını belirten özellik
    }
}
