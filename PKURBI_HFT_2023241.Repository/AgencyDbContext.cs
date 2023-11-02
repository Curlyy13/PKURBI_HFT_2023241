using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PKURBI_HFT_2023241.Models;

namespace PKURBI_HFT_2023241.Repository
{
    public class AgencyDbContext : DbContext
    {
        public DbSet<Property> properties { get; set; }
        public DbSet<Tenant> tenants { get; set; }
        public DbSet<Salesperson> salespeople { get; set; }

        public AgencyDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Agency.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1 a többhöz kapcsolat : 1 Salesperson-nek több property-je  is lehet
            modelBuilder.Entity<Property>(property => property
                    .HasOne(property => property.Salesperson)
                    .WithMany(Salesperson => Salesperson.Properties)
                    .HasForeignKey(property => property.SalesId)
                    .OnDelete(DeleteBehavior.Cascade));


            //1 a többhöz kapcsolat : 1 Tenant-nak több propery-je is lehet
            modelBuilder.Entity<Property>(property => property
                    .HasOne(property => property.Tenant)
                    .WithMany(Tenant => Tenant.Properties)
                    .HasForeignKey(property => property.TenantId)
                    .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
