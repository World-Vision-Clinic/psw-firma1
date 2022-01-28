using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<FullName>(entity =>
            {
                entity.HasAlternateKey(x => x.Id);
            });

            modelBuilder.Entity<Residence>(entity =>
            {
                entity.HasAlternateKey(x => x.Id);
            });*/

            modelBuilder.Entity<Patient>().HasData(
                new Patient(4, "Marko123", "123", new FullName("Marko", "Markovic"), "markomarkovic@gmail.com", true, Gender.Male, "1637597",
                    DateTime.Now, new Residence("Serbia", "TestAddress", "TestCity"), "063115111", 10, 80, 180, BloodType.A, false, new List<Appointment>(), "")
                );
            modelBuilder.Entity<Appointment>().HasData(
            new Appointment(199, 4, 10, DateTime.Now.AddDays(-1), false, AppointmentType.Appointment)
            );
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
        }

    }
}
