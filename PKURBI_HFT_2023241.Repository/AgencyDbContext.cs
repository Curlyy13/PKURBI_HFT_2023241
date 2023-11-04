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

            //Tesztadatok feltöltése
            //Formátum : "PropId#Város#Érték#Alapterület#SalesID#TenantId"
            modelBuilder.Entity<Property>().HasData(new Property[]
                {
                new Property("1#Budapest#265000#150#2#1"),
                new Property("2#Washington#1200000#200#5#3"),
                new Property("3#Roma#200000#80#3#6"),
                new Property("4#Budapest#100000#60#7#5"),
                new Property("5#Berlin#800000#140#6#2"),
                new Property("6#London#900000#120#8#9"),
                new Property("7#London#900000#120#5#4"),
                new Property("8#London#900000#120#3#7"),
                new Property("9#London#900000#120#2#8")
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
                new Tenant("1#Olivia Briggs#7088422114#5"),
                new Tenant("2#Bennett Parks#70218552514#2"),
                new Tenant("3#Les Kain#7085475114#3"),
                new Tenant("4#Brigham Glisson#7087482912#8"),
                new Tenant("5#Edwin Porter#8835214478#1"),
                new Tenant("6#Irene Thompson#7185324364#4"),
                new Tenant("7#Oriel Hall#9034728491#9"),
                new Tenant("8#William Wood#8143858122#6"),
                new Tenant("9#Laurence Mccoy#3654223414#7")
            });
        }
    }
}
