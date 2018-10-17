using Microsoft.AspNetCore.Identity;

namespace BreakingNews.Presentation.AspNetCore
{
    public static class Seed
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }


        private static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                var role = new IdentityRole("admin");
                roleManager.CreateAsync(role).Wait();
            }

            if (!roleManager.RoleExistsAsync("user").Result)
            {
                var role = new IdentityRole("user");
                roleManager.CreateAsync(role).Wait();
            }
        }

        private static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("admin@breakingnewsapp.com").Result == null)
            {
                var user = new IdentityUser("admin@breakingnewsapp.com");

                var result = userManager.CreateAsync(user, "Qwerty!1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("user@breakingnewsapp.com").Result == null)
            {
                var user = new IdentityUser("user@breakingnewsapp.com");

                var result = userManager.CreateAsync(user, "Qwerty!1234").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user").Wait();
                }
            }
        }
    }
}
