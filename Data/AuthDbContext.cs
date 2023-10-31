using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Techblog.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = "03b6e943-0167-4199-bfd5-ede1ac49c4fb";
            var SuperRoleId = "93c453a6-ea61-4da5-934f-05e6e061ccc5";
            var UserRoleId = "1ba837ab-ed17-4317-adf6-eb714a5797d3";

            var roles = new List<IdentityRole>
            {

                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId
                },
                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = SuperRoleId,
                    ConcurrencyStamp = SuperRoleId
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = UserRoleId,
                    ConcurrencyStamp = UserRoleId
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);


            var superAdminId = "30b7baff-96dc-40a3-935a-3cbed48b74cc";
            var SuperAdmin = new IdentityUser
            {
                UserName = "superadmin@techbg.com",
                Email = "superadmin@techbg.com",
                NormalizedEmail = "SuperAdmin@techbg.com".ToUpper(),
                NormalizedUserName = "SuperAdmin@techbg.com".ToUpper(),
                Id = superAdminId
            };

            SuperAdmin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(SuperAdmin, "super@123");

            builder.Entity<IdentityUser>().HasData(SuperAdmin);

            var superAdminRoles = new List<IdentityUserRole<string>>
            {
                 new IdentityUserRole<string>
                {
                  RoleId = adminRoleId,
                  UserId = superAdminId
                },
                  new IdentityUserRole<string>
                {
                  RoleId = SuperRoleId,
                  UserId = superAdminId
                },
                   new IdentityUserRole<string>
                {
                  RoleId = UserRoleId,
                  UserId = superAdminId
                },
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }
    }
}
