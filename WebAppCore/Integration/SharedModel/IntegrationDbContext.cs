using System;
using System.Collections.Generic;
using System.Text;
using Integration.Pharmacy.Model;
using Microsoft.EntityFrameworkCore;

namespace Integration.SharedModel
{
    public class IntegrationDbContext : DbContext
    {

        public DbSet<Objection> Objections { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<PharmacyProfile> Pharmacies { get; set; }
        public IntegrationDbContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=IntegrationDatabase;User Id=postgres;Password=admin");
        }


    }
}
