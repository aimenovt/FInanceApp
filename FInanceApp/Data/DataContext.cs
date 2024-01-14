using FInanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FInanceApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> o) : base(o)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Balance> Balances { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FinGoal> FinGoals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasOne(u => u.Balance).WithOne(b => b.User).HasForeignKey<Balance>(b => b.UserId);



            //// INDEXES EXAMPLE
            //modelBuilder.Entity<Country>(entity =>
            //{
            //    // Single-column index
            //    entity.HasIndex(e => e.Name);

            //    // Composite index on multiple columns
            //    entity.HasIndex(e => new { e.Name, e.Id });

            //    // Unique index
            //    entity.HasIndex(e => e.Name).IsUnique();

            //    //Clustered index
            //    entity.HasIndex(e => e.Name).IsClustered();

            //    //Non-Clustered index + Unique
            //    entity.HasIndex(e => e.Name).IsUnique().IsClustered(false);

            //    //Filtered index
            //    entity.HasIndex(e => e.Name).HasFilter("[Name] IS NOT NULL");
            //});
        }
    }
}
