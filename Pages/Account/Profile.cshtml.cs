using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Data;
using Odev_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Odev_1.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;

        public ProfileModel(UserManager<AppUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public ProfileInputModel ProfileInput { get; set; } = new ProfileInputModel();

        [BindProperty]
        public string NewSupportMessage { get; set; } = string.Empty;

        public List<SupportRequest> SupportRequests { get; set; } = new List<SupportRequest>();

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Index");

            ProfileInput = new ProfileInputModel
            {
                FullName = user.FullName,
                Email = user.Email
            };

            SupportRequests = _context.SupportRequests
                .Where(s => s.UserId == user.Id)
                .OrderByDescending(s => s.CreatedAt)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostSendSupportMessageAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToPage("/Index");

            var supportRequest = new SupportRequest
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Message = NewSupportMessage
            };

            _context.SupportRequests.Add(supportRequest);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Destek mesajınız başarıyla gönderildi!";
            return RedirectToPage();
        }
    }

    public class ProfileInputModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
