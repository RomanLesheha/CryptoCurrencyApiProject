using Coursework2.Models.Constants;
using Microsoft.AspNetCore.Identity;

namespace Coursework2.Areas.Identity.Data
{
    public class DBSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider serviceProvider)
        {
            var userMng = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleMng = serviceProvider.GetService<RoleManager<IdentityRole>>();

            await roleMng.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMng.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var admin = new IdentityUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMng.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userMng.CreateAsync(admin, "Admin@123");
                await userMng.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}
