using Hospital.MedicalRecords.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.SharedModel
{
    public class EventsDbContext : DbContext
    {
        public DbSet<Event> EventsHospital{ get; set; }

        public EventsDbContext()
        {
        }
        public EventsDbContext(DbContextOptions<EventsDbContext> options) : base(options) { }

        public EventsDbContext(DbContextOptions<TestEventsDbContext> options) : base(options) { }

        public EventsDbContext(DbSet<Event> events)
        {
            EventsHospital = events;
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            /*modelbuilder.Entity<Event>().HasData(
               new Event(1, "Klik", DateTime.Now)
                );*/
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
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "admin";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "ftn";

            return
                $"Server={server};Port={port};Database={database};User ID={user};Password={password};";

        }
    }
}
