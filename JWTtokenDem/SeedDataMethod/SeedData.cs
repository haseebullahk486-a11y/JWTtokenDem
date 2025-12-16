using JWTtokenDem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JWTtokenDem.SeedDataMethod
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // GUIDs for roles and user
            const string ADMIN_ROLE_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            const string USER_ROLE_ID = "a18bf10b-aa65-189f-bd17-00bd9344e575";

            //const string MANAGER_ROLE_ID = "a18bf10b-aa65-4af8-bd17-00bd9344e575";
            //const string MANAGER_ID = "U53b01-aa65-189f-14pd-00bd9344e575";

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = ADMIN_ROLE_ID, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = USER_ROLE_ID, Name = "User", NormalizedName = "USER" }
                //new IdentityRole { Id = CUSTOMER_ROLE_ID, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // Password hasher for user password
            var hasher = new PasswordHasher<User>();

            // Seed Manager user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = ADMIN_ROLE_ID,
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@gmail.com",
                    NormalizedEmail = "ADMIN@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin@123"),
                    SecurityStamp = string.Empty,
                    Password = "Admin@123",
                    City = "Islamabad"
                  

                }
            );

            // Assign Manager user to Manager role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ROLE_ID
                }
            );
        }
    }
}
