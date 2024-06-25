using Agri_Energy_Connect.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Agri_Energy_Connect.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        // Model contexts:
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Model creating method for linking tables
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This is important to call to configure the identity model

            // Makes FarmerId a foreign key in Products table
            modelBuilder.Entity<Product>()
                .HasOne(m => m.Farmer)
                .WithMany(s => s.Products)
                .HasForeignKey(m => m.FarmerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
