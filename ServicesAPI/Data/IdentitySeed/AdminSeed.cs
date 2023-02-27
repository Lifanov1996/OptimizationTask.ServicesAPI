using Microsoft.AspNetCore.Identity;
using ServicesAPI.Data.Entity;
using ServicesAPI.Models.Users;

namespace ServicesAPI.Data.IdentitySeed
{
    public class AdminSeed
    {
        public static async Task SeedDataAsync(ContextDB context, UserManager<User> userManager)
        {
            if (!userManager.Users.Any())
            {
                var user = new User { UserName = "Admin" };
                await userManager.CreateAsync(user, "Admin123!");
                //var users = new List<AppUser>
                //            {
                //                new AppUser
                //                    {
                //                        DisplayName = "TestUserFirst",
                //                        UserName = "TestUserFirst",
                //                        Email = "testuserfirst@test.com"
                //                    },

                //                new AppUser
                //                    {
                //                        DisplayName = "TestUserSecond",
                //                        UserName = "TestUserSecond",
                //                        Email = "testusersecond@test.com"
                //                    }
                //              };

                //foreach (var user in users)
                //{
                //    await userManager.CreateAsync(user, "qazwsX123@");
                //}
            }
        }
    }
}
