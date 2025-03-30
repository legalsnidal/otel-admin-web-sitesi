using Microsoft.AspNetCore.Mvc.RazorPages;
using Odev_1.Data;

namespace Odev_1.Pages.Admin
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }
    }
}
