using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
     public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.Price)
                    .HasPrecision(18, 2);

                entity.ToTable(table =>
                    table.HasCheckConstraint(
                        "CK_Products_Price",
                        "[Price] > 0"));
            });
        }

    }
}
