using System;
using System.Collections.Generic;
using System.Text;
using Integration.Pharmacy.Model;
using Microsoft.EntityFrameworkCore;

namespace Integration.SharedModel
{
    public class IntegrationDbContext : DbContext
    {

        public DbSet<Objection> Object { get; set; }
        public DbSet<Reply> Reply { get; set; }
        public DbSet<Pharmacy.Model.Pharmacy> Pharmacy { get; set; }
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
