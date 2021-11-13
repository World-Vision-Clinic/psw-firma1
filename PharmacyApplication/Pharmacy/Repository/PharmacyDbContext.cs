using Microsoft.EntityFrameworkCore;
using Pharmacy.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Repository
{
    public class PharmacyDbContext : DbContext
    {
        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<Objection> Objections { get; set; }
        public DbSet<Reply> Replies { get; set; }

        public PharmacyDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=PharmacyDatabase;User id=postgres;Password=admin");
        }


    }
}
