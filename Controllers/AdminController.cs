using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Odev_1.Models;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public AdminController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        var users = _userManager.Users.ToList();
        return Ok(users);
    }

    [HttpPost("delete-user")]
    public async Task<IActionResult> DeleteUser([FromForm] string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Kullanıcı bulunamadı.");
        }

        await _userManager.DeleteAsync(user);
        return Ok("Kullanıcı silindi.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromForm] string userId, [FromForm] string newPassword)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound("Kullanıcı bulunamadı.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (result.Succeeded)
        {
            return Ok("Şifre başarıyla güncellendi.");
        }

        return BadRequest("Şifre sıfırlama başarısız.");
    }
}
