using System;
using System.Collections.Generic;
using System.Text;
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

        public IntegrationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=IntegrationDatabase;User Id=postgres;Password=admin");
            //optionsBuilder.UseNpgsql("Server=integrationapi;Port=5432;Database=IntegrationDatabase;User Id=postgres;Password=admin");
            optionsBuilder.UseNpgsql(CreateConnectionStringFromEnvironment());
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
