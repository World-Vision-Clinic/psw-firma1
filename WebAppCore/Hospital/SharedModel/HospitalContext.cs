using Hospital;
using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.SharedModel
{
    public class HospitalContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyQuestion> Questions { get; set; }
        public DbSet<AnsweredSurveyQuestion> AnsweredQuestions { get; set; }
        public DbSet<Appointment> Appointmnets { get; set; }


        public HospitalContext()
        {
         
        }


        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public HospitalContext(DbContextOptions<TestContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Surveys");
                entity.HasKey(c => c.IdSurvey);

                entity.HasOne(d => d.Appointment)
               .WithMany(p => p.Surveys)
               .HasForeignKey(d => d.IdAppointment);
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointments");
                entity.HasKey(c => c.IdAppointment);            

            });

           





        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
        }
    }
}
