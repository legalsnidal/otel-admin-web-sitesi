using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Models;
using Odev_1.Data;

namespace Odev_1.Pages.Admin
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public new AppUser User { get; set; } = new AppUser(); // Hata düzetildi

        public IActionResult OnGet(string id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            User = user;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userInDb = _context.Users.Find(User.Id);
            if (userInDb != null)
            {
                userInDb.FullName = User.FullName;
                userInDb.Email = User.Email;
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
