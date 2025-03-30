using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Odev_1.Models
{
    public class SupportMessage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }

        public string? AdminResponse { get; set; }
        public bool IsResolved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
