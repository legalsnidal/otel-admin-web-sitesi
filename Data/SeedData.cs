using Odev_1.Models;  // Bu namespace'i doğru ekleyin
using Microsoft.AspNetCore.Identity;

namespace Odev_1.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, UserManager<AppUser> userManager)
        {
            var user = new AppUser
            {
                UserName = "admin",
                Email = "admin@example.com",
                FullName = "Admin User"  // Burada FullName özelliğini kullanabilirsin
            };

            if (userManager.Users.All(u => u.UserName != user.UserName))
            {
                await userManager.CreateAsync(user, "Test123!");
            }
        }
    }
}
