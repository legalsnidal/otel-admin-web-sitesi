using System;
using System.ComponentModel.DataAnnotations;

namespace Odev_1.Models
{
    public class SupportRequest
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Message { get; set; } = string.Empty;

        public string? AdminReply { get; set; }  // ✅ Admin'in yanıtı için alan eklendi

        public bool IsResolved { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
