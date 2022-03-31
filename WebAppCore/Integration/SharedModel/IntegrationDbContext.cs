using System;
using System.Collections.Generic;
using System.Text;
using Integration.Partnership.Model;
using Integration.Partnership.Repository;
using Integration.Pharmacy.Model;
using Microsoft.EntityFrameworkCore;

namespace Integration.SharedModel
{
    public class IntegrationDbContext : DbContext
    {
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Objection> Objections { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<PharmacyProfile> Pharmacies { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<TenderItem> TenderItems { get; set; }
        public DbSet<TenderOffer> TenderOffers { get; set; }
        public DbSet<OfferItem> OfferItems { get; set; }

        public IntegrationDbContext()
        {

        }

        public IntegrationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Tender>()
            .HasMany(i => i.TenderItems)
            .WithOne();
            modelbuilder.Entity<Tender>()
            .HasMany(i => i.TenderOffers)
            .WithOne();
            modelbuilder.Entity<TenderOffer>()
            .HasMany(o => o.OfferItems)
            .WithOne();

            modelbuilder.Entity<News>()
            .OwnsOne(n => n.DateRange)
            .WithOwner();
            modelbuilder.Entity<PharmacyProfile>()
            .OwnsOne(n => n.ConnectionInfo)
            .WithOwner();
            modelbuilder.Entity<PharmacyProfile>()
            .OwnsOne(n => n.Address)
            .WithOwner();

        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = CreateConnectionStringFromEnvironment();
                optionsBuilder.UseNpgsql(connectionString);
            }
        }


        private static string CreateConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "IntegrationDatabase";
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "admin";

            return
                $"Server={server};Port={port};Database={database};User ID={user};Password={password};";
        }
    }
}
