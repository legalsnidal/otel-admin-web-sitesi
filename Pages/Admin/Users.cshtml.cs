using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Models;
using System.Collections.Generic;
using System.Linq;

namespace Odev_1.Pages.Admin
{
    public class UsersModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;

        public UsersModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public List<AppUser> Users { get; set; } = new();

        public void OnGet()
        {
            Users = _userManager.Users.ToList();
        }
    }
}
