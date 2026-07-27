using Domain.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            // Check before creating — prevents errors on subsequent app restarts
            if (!await roleManager.RoleExistsAsync(SystemRoles.SuperAdmin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(SystemRoles.SuperAdmin.ToString()));

            if (!await roleManager.RoleExistsAsync(SystemRoles.User.ToString()))
                await roleManager.CreateAsync(new IdentityRole(SystemRoles.User.ToString()));
        }
    }
}
