using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> EventsPharmacy { get; set; }

        public EventsDbContext()
        {
        }

        public EventsDbContext(DbSet<Event> events)
        {
            EventsPharmacy = events;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Event>().HasData(
               new Event(1, "Klik", DateTime.Now)
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(CreateConnectionStringFromEnvironment()).UseLazyLoadingProxies();
        }

        private string CreateConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "EventsDatabase";
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "postgres";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "admin";

            return
                $"Server={server};Port={port};Database={database};User ID={user};Password={password};";

        }
    }
}
