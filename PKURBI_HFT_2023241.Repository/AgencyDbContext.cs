using System;
using System.Data.SqlTypes;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PKURBI_HFT_2023241.Models;

namespace PKURBI_HFT_2023241.Repository
{
    public class AgencyDbContext : DbContext
    {
        public DbSet<RealEstate> realEstates { get; set; }
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
                optionsBuilder
                    .UseInMemoryDatabase("AgencyDB")
                    .UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1 a többhöz kapcsolat : 1 Salesperson-nek több realestate-je  is lehet
            modelBuilder.Entity<RealEstate>(realestate => realestate
                    .HasOne(realestate => realestate.Salesperson)
                    .WithMany(Salesperson => Salesperson.Realestates)
                    .HasForeignKey(realestate => realestate.SalesId)
                    .OnDelete(DeleteBehavior.Cascade));


            //1 a többhöz kapcsolat : 1 Tenant-nak több realestate-je is lehet
            modelBuilder.Entity<RealEstate>(realestate => realestate
                    .HasOne(realestate => realestate.Tenant)
                    .WithMany(Tenant => Tenant.Realestates)
                    .HasForeignKey(realestate => realestate.TenantId)
                    .OnDelete(DeleteBehavior.Cascade));

            //Tesztadatok feltöltése
            //Formátum : "RealEstateId#Város#Érték#Alapterület#SalesID#TenantId"
            modelBuilder.Entity<RealEstate>().HasData(new RealEstate[]
                {
                new RealEstate("1#Budapest#265000#150#2#1"),
                new RealEstate("2#Washington#1200000#200#4#3"),
                new RealEstate("3#Roma#200000#80#3#1"),
                new RealEstate("4#Budapest#100000#60#2#4"),
                new RealEstate("5#Berlin#800000#140#4#2"),
                new RealEstate("6#London#900000#120#4#1"),
                new RealEstate("7#London#800000#130#1#2"),
                new RealEstate("8#London#750000#150#3#1"),
                new RealEstate("9#London#900000#120#2#4")
                });

            //Formátum : "SalesId#Név#Kor"
            modelBuilder.Entity<Salesperson>().HasData(new Salesperson[]
            {
                new Salesperson("1#John Miller#30"),
                new Salesperson("2#David Holmes#50"),
                new Salesperson("3#Peter Parker#34"),
                new Salesperson("4#Lázár Vilmos#42"),
            });

            //Formátum : "TenantID#Name#Phone"
            modelBuilder.Entity<Tenant>().HasData(new Tenant[]
            {
                new Tenant("1#Olivia Briggs#108842211"),
                new Tenant("2#Bennett Parks#102185525"),
                new Tenant("3#Les Kain#108547511"),
                new Tenant("4#Brigham Glisson#108748291"),
            });
        }
    }
}
