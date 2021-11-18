using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Models
{
    public class HospitalContext : DbContext
    {
        public DbSet<Feedback> Feedbacks{ get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveySection> SurveySections { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; }
        
        public HospitalContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {

        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
        }
    }
}
