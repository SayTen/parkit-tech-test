using Microsoft.EntityFrameworkCore;
using Parkit.Core.DAL.Contexts.Models;

namespace Parkit.Core.DAL.Contexts
{
    internal class UserContext : DbContext
    {
        public UserContext()
        { }

        public UserContext(DbContextOptions<UserContext> dbContextOptions)
            : base(dbContextOptions)
        { }

        public DbSet<User> Users => Set<User>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlite("Data Source=:memory:");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}
