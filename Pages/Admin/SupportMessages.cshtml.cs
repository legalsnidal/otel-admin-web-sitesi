using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Odev_1.Data;
using Odev_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odev_1.Pages.Admin
{
    public class SupportMessagesModel : PageModel
    {
        private readonly AppDbContext _context;

        public SupportMessagesModel(AppDbContext context)
        {
            _context = context;
        }

        public List<SupportRequest> SupportRequests { get; set; } = new List<SupportRequest>();

        public async Task<IActionResult> OnGetAsync()
        {
            SupportRequests = await _context.SupportRequests
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostReplyAsync(int SupportRequestId, string AdminResponse)
        {
            var request = await _context.SupportRequests.FindAsync(SupportRequestId);
            if (request != null)
            {
                request.AdminResponse = AdminResponse;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostResolveAsync(int SupportRequestId)
        {
            var request = await _context.SupportRequests.FindAsync(SupportRequestId);
            if (request != null)
            {
                request.IsResolved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}
