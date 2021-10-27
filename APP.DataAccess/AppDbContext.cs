using APP.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace APP.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public AppDbContext() : base()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Companies");
            modelBuilder.Entity<Company>().HasKey(p => p.ID).IsClustered(true);
            modelBuilder.Entity<Company>().Property(p => p.ID).UseIdentityColumn(1, 1);
            modelBuilder.Entity<Company>().Property(p => p.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Company>().Property(p => p.Code).HasMaxLength(2).IsRequired();
            modelBuilder.Entity<Company>().Property(p => p.PhoneNumber);
            modelBuilder.Entity<Company>().Property(p => p.CompanyPhoto).HasColumnType("VARBINARY(MAX)");


            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(p => p.ID).IsClustered(true);
            modelBuilder.Entity<Customer>().Property(p => p.ID).UseIdentityColumn(1, 1);
            modelBuilder.Entity<Customer>().Property(p => p.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.Job).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.MobileNo);
            modelBuilder.Entity<Customer>().Property(p => p.Email).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Customer>().Property(p => p.BirthDate);
            modelBuilder.Entity<Customer>().HasOne(p => p.Company).WithMany().HasForeignKey(p => p.CompanyId);
        }
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Added)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in added)
            {
                if (entity is BaseEntity baseEntity)
                    baseEntity.CreationDate = DateTime.UtcNow;
            }

            var modified = ChangeTracker.Entries()
                        .Where(t => t.State == EntityState.Modified)
                        .Select(t => t.Entity)
                        .ToArray();

            foreach (var entity in modified)
            {
                if (entity is BaseEntity baseEntity)
                    baseEntity.UpdateDate = DateTime.UtcNow;
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
    }
}
