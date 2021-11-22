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
        public DbSet<Appointment> Appointments { get; set; }

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
                entity.HasKey(c => c.Id);            

            });

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment { Id = 1, Surveys = new List<Survey>() }
                );

            modelBuilder.Entity<Survey>().HasData(
                new Survey { IdAppointment = 1, CreationDate = DateTime.Now, IdSurvey = 1}
                );

            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion { Id = 1, Question = "Has doctor been polite to you?", Section = SurveySectionType.Doctor, IdSurvey = 1},
                new SurveyQuestion { Id = 2, Question = "How would you rate the professionalism of doctor?", Section = SurveySectionType.Doctor, IdSurvey = 1 },
                new SurveyQuestion { Id = 3, Question = "How clearly did the doctor explain you your condition?", Section = SurveySectionType.Doctor, IdSurvey = 1 },
                new SurveyQuestion { Id = 4, Question = "How would you rate the doctor's patience with you?", Section = SurveySectionType.Doctor, IdSurvey = 1 },
                new SurveyQuestion { Id = 5, Question = "What is your overall satisfaction with doctor?", Section = SurveySectionType.Doctor, IdSurvey = 1 },

                new SurveyQuestion { Id = 6, Question = "How easy is to use our application?", Section = SurveySectionType.Hospital, IdSurvey = 1 },
                new SurveyQuestion { Id = 7, Question = "How easy it was to schedule an appointment?", Section = SurveySectionType.Hospital, IdSurvey = 1 },
                new SurveyQuestion { Id = 8, Question = "What is an opportunity to recommend us to your friends and family?", Section = SurveySectionType.Hospital, IdSurvey = 1 },
                new SurveyQuestion { Id = 9, Question = "How satisfied are you with the services that the hospital provides you?", Section = SurveySectionType.Hospital, IdSurvey = 1 },
                new SurveyQuestion { Id = 10, Question = "What is your overall satisfaction with our hospital?", Section = SurveySectionType.Hospital, IdSurvey = 1 },

                new SurveyQuestion { Id = 11, Question = "How would you rate the kindness of our staff?", Section = SurveySectionType.Staff, IdSurvey = 1 },
                new SurveyQuestion { Id = 12, Question = "How would you rate the professionalism of our staff?", Section = SurveySectionType.Staff, IdSurvey = 1 },
                new SurveyQuestion { Id = 13, Question = "How clearly did the staff explain you some procedures of our hospital?", Section = SurveySectionType.Staff, IdSurvey = 1 },
                new SurveyQuestion { Id = 14, Question = "How yould you rate to what extent staff was available to you during your visit to the hospital?", Section = SurveySectionType.Staff, IdSurvey = 1 },
                new SurveyQuestion { Id = 15, Question = "What is your overall satisfaction with our staff?", Section = SurveySectionType.Staff, IdSurvey = 1 }
                );

        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
        }
    }
}
