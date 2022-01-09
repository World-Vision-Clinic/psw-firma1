using Integration.Pharmacy.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.SharedModel
{
    public class EventDbContext : DbContext
    {
        public DbSet<Event> EventsIntegration { get; set; }

        public EventDbContext()
        {
        }
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public EventDbContext(DbSet<Event> events)
        {
            EventsIntegration = events;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Event>().HasData(
               new Event(1, "Klik", DateTime.Now)
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = CreateConnectionStringFromEnvironment();
                optionsBuilder.UseNpgsql(connectionString);
            }
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

