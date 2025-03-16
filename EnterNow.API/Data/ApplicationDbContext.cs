using EnterNow.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EnterNow.API.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Membership> Memberships { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            var adminRole = new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" };
            var memberRole = new IdentityRole { Id = "2", Name = "Member", NormalizedName = "MEMBER" };

            builder.Entity<IdentityRole>().HasData(adminRole, memberRole);

            var hasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = "admin-1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@thegym.com",
                NormalizedEmail = "ADMIN@THEGYM.COM",
                EmailConfirmed = true,
                MembershipExpiry = DateTime.UtcNow.AddYears(1),
                IsPaymentCurrent = true
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin1");

            builder.Entity<ApplicationUser>().HasData(adminUser);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = "admin-1", RoleId = "1" });


            var memberUser = new ApplicationUser
            {
                Id = "member-1",
                UserName = "member",
                NormalizedUserName = "MEMBER",
                Email = "member@thegym.com",
                NormalizedEmail = "MEMBER@THEGYM.COM",
                EmailConfirmed = true,
                MembershipExpiry = DateTime.UtcNow.AddYears(1),
                IsPaymentCurrent = true
            };
            memberUser.PasswordHash = hasher.HashPassword(memberUser, "Member1");

            builder.Entity<ApplicationUser>().HasData(memberUser);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = "member-1", RoleId = "2" });
        }
    }
}
