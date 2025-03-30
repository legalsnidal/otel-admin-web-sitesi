using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Models;
using System.Threading.Tasks;

namespace Odev_1.Pages.Admin
{
    public class DeleteUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public DeleteUserModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToPage("/Admin/Users");
            }

            await _userManager.DeleteAsync(user);
            return RedirectToPage("/Admin/Users");
        }
    }
}
