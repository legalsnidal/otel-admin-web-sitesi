using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Odev_1.Data;
using Odev_1.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Odev_1.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(UserManager<AppUser> userManager, AppDbContext context, ILogger<IndexModel> logger)
        {
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public List<AppUser> Users { get; set; } = new List<AppUser>();
        public List<Reservation> PendingReservations { get; set; } = new List<Reservation>();
        public List<SupportRequest> SupportMessages { get; set; } = new List<SupportRequest>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!User.IsInRole("Admin"))
            {
                return RedirectToPage("/Index");
            }

            Users = await _userManager.Users.ToListAsync();

            // 📌 Bekleyen rezervasyonları çekiyoruz
            PendingReservations = await _context.Reservations
                .Where(r => r.Status == ReservationStatus.Pending)
                .OrderByDescending(r => r.CheckInDate)
                .ToListAsync();

            // 📌 Destek mesajlarını çekiyoruz
            SupportMessages = await _context.SupportRequests
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();

            _logger.LogInformation($"[Admin Paneli] {PendingReservations.Count} bekleyen rezervasyon, {SupportMessages.Count} destek mesajı bulundu.");

            return Page();
        }

        public async Task<IActionResult> OnPostReplySupportMessageAsync(int messageId, string adminReply)
        {
            if (string.IsNullOrWhiteSpace(adminReply))
            {
                ModelState.AddModelError("", "Yanıt boş olamaz!");
                return Page();
            }

            var message = await _context.SupportRequests.FindAsync(messageId);
            if (message != null)
            {
                message.AdminReply = adminReply;  // ✅ Admin cevabını kaydet
                message.IsResolved = true; // ✅ Destek mesajı çözüldü olarak işaretlendi
                await _context.SaveChangesAsync();

                _logger.LogInformation($"[Admin Paneli] Destek mesajı yanıtlandı. (ID: {messageId})");
            }
            else
            {
                _logger.LogWarning($"[Admin Paneli] Yanıt verilmeye çalışılan destek mesajı bulunamadı. (ID: {messageId})");
            }

            return RedirectToPage();
        }
    }
}
