﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Substance> Substances { get; set; }
        public DbSet<SubstituteMedicine> SubstituteMedicines { get; set; }
        public DbSet<News> News { get; set; }

        public PharmacyDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

            modelbuilder.Entity<SubstituteMedicine>().HasKey(sm => new { sm.MedicineId, sm.SubstituteId });
            modelbuilder.Entity<SubstituteMedicine>()
                .HasOne(pt => pt.Substitute)
                .WithMany()
                .HasForeignKey(pt => pt.SubstituteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelbuilder.Entity<SubstituteMedicine>()
                .HasOne(pt => pt.Medicine)
                .WithMany(t => t.SubstituteMedicines)
                .HasForeignKey(pt => pt.MedicineId);

            modelbuilder.Entity<Substance>()
                .HasKey(s => new { s.SubstanceId, s.MedicineId });

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=PharmacyDatabase;User id=postgres;Password=admin").UseLazyLoadingProxies();
        }


    }
}
