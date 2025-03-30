using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Odev_1.Models;

namespace Odev_1.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [BindProperty]
        public LoginInputModel Input { get; set; } = new LoginInputModel();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                TempData["LoginError"] = "Bu bilgilere ait bir hesap bulunamadı!";
                return Page();
            }

            var result = await _signInManager.PasswordSignInAsync(user, Input.Password, false, false);

            if (result.Succeeded)
            {
                TempData["LoginSuccess"] = "Başarıyla giriş yaptınız!";
                return RedirectToPage("/Index");
            }
            else
            {
                TempData["LoginError"] = "Şifre hatalı!";
                return Page();
            }
        }
    }

    public class LoginInputModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
