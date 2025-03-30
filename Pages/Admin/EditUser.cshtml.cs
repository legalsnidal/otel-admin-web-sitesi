using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Models;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Odev_1.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public EditUserModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Id { get; set; }

            [Required]
            public string FullName { get; set; }

            [Required, EmailAddress]
            public string Email { get; set; }

            [Required]
            public string PhoneNumber { get; set; }

            public string Country { get; set; }
            public string IdentityNumber { get; set; }
            public DateTime? BirthDate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Kullanıcı bilgilerini sayfaya gönder
            Input = new InputModel
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                IdentityNumber = user.IdentityNumber,
                BirthDate = user.BirthDate
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(Input.Id);
            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            // Kullanıcı bilgilerini güncelle
            user.FullName = Input.FullName;
            user.Email = Input.Email;
            user.PhoneNumber = Input.PhoneNumber;
            user.Country = Input.Country;
            user.IdentityNumber = Input.IdentityNumber;
            user.BirthDate = Input.BirthDate;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }

            TempData["SuccessMessage"] = "Kullanıcı bilgileri başarıyla güncellendi.";
            return RedirectToPage("/Admin/Users");
        }
    }
}
