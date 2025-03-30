using System;
using System.ComponentModel.DataAnnotations;

namespace Odev_1.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string RoomType { get; set; } = string.Empty;

        [Required]
        public int GuestCount { get; set; }

        [Required]
        public int BedCount { get; set; }

        [Required]
        public int RoomCount { get; set; }

        [Required]
        public DateTime CheckInDate { get; set; }

        [Required]
        public DateTime CheckOutDate { get; set; }

        [Required]
        [Range(1, 999999)]
        public decimal TotalPrice { get; set; }

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        public string? RejectionReason { get; set; }
    }

    // 📌 **EKSİK OLAN ENUM BURAYA EKLENDİ**
    public enum ReservationStatus
    {
        Pending,   // Beklemede
        Approved,  // Onaylandı
        Rejected   // Reddedildi
    }
}
