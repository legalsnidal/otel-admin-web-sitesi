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

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public bool IsAdmin { get; set; } = false; // Burada default değer verdik
    }
}
