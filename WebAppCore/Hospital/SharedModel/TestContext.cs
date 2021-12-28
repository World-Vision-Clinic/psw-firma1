using Hospital.MedicalRecords.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.SharedModel
{
    public class TestContext : HospitalContext
    {

        public TestContext()
        {

        }

        public TestContext(DbContextOptions<TestContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<FullName>().HasNoKey();

            modelbuilder.Entity<Patient>(entity =>
            {
                entity.HasKey(c => c.Id);

                entity.OwnsOne(x => x.FullName);
                entity.OwnsOne(x => x.Residence);
            });
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
