using JWTtokenDem.Models;
using JWTtokenDem.SeedDataMethod;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JWTtokenDem.Data
{
    public class MyDbContext:IdentityDbContext<User>
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
          : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Seed();
        }
        public DbSet<User> User { get; set; }
    }
}
