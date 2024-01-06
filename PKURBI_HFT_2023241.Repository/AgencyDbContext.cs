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
                string conn =
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Agency.mdf;Integrated Security=True;MultipleActiveResultSets=true";
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(conn);
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
                new RealEstate("2#Washington#1200000#200#5#3"),
                new RealEstate("3#Roma#200000#80#3#6"),
                new RealEstate("4#Budapest#100000#60#7#5"),
                new RealEstate("5#Berlin#800000#140#6#2"),
                new RealEstate("6#London#900000#120#8#9"),
                new RealEstate("7#London#900000#120#5#4"),
                new RealEstate("8#London#900000#120#3#7"),
                new RealEstate("9#London#900000#120#2#8")
                });

            //Formátum : "SalesId#Név#Kor#PropId"
            modelBuilder.Entity<Salesperson>().HasData(new Salesperson[]
            {
                new Salesperson("1#John Miller#30#3"),
                new Salesperson("2#David Holmes#50#6"),
                new Salesperson("3#Peter Parker#34#2"),
                new Salesperson("4#Lázár Vilmos#42#1"),
                new Salesperson("5#Kovács István#20#4"),
                new Salesperson("6#Mike Cenat#24#7"),
                new Salesperson("7#John Davis#23#8"),
                new Salesperson("8#David D.#43#9"),
                new Salesperson("9#Joe Trump#44#10"),
            });

            //Formátum : "TenantID#Name#Phone#PropID"
            modelBuilder.Entity<Tenant>().HasData(new Tenant[]
            {
                new Tenant("1#Olivia Briggs#708842211#5"),
                new Tenant("2#Bennett Parks#702185525#2"),
                new Tenant("3#Les Kain#708547511#3"),
                new Tenant("4#Brigham Glisson#708748291#8"),
                new Tenant("5#Edwin Porter#883521447#1"),
                new Tenant("6#Irene Thompson#718532364#4"),
                new Tenant("7#Oriel Hall#903472841#9"),
                new Tenant("8#William Wood#814385122#6"),
                new Tenant("9#Laurence Mccoy#365423414#7")
            });
        }
    }
}
