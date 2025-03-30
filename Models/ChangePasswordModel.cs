using System.ComponentModel.DataAnnotations;

namespace Odev_1.Models
{
    public class ChangePasswordModel
    {
        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Yeni şifreler eşleşmiyor!")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
