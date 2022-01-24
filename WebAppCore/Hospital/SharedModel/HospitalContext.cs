using Hospital;
using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using Hospital.ShiftsAndVacations.Model;

namespace Hospital.SharedModel
{
    public class HospitalContext : DbContext
    {
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<SurveyQuestion> Questions { get; set; }
        public DbSet<AnsweredSurveyQuestion> AnsweredQuestions { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<PatientAllergen> PatientAllergens { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<OutsideDoor> OutsideDoors { get; set; }
        public DbSet<MapPosition> MapPositions { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<FloorLabel> FloorLabels { get; set; }
        public DbSet<Equipment> AllEquipment { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Therapy> Therapies { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }



        public HospitalContext() { }

        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }
        public HospitalContext(DbContextOptions<TestContext> options) : base(options) { }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Survey>(entity =>
            {
                entity.ToTable("Surveys");
                entity.HasKey(c => c.Id);
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
                new Survey(1, DateTime.Now)
            );

            modelBuilder.Entity<SurveyQuestion>().HasData(
                new SurveyQuestion(1, "Has doctor been polite to you?", SurveySectionType.Doctor),
                new SurveyQuestion(2, "How would you rate the professionalism of doctor?", SurveySectionType.Doctor),
                new SurveyQuestion(3, "How clearly did the doctor explain you your condition?", SurveySectionType.Doctor),
                new SurveyQuestion(4, "How would you rate the doctor's patience with you?", SurveySectionType.Doctor),
                new SurveyQuestion(5, "What is your overall satisfaction with doctor?", SurveySectionType.Doctor),

                new SurveyQuestion(6, "How easy is to use our application?", SurveySectionType.Hospital),
                new SurveyQuestion(7, "How easy it was to schedule an appointment?", SurveySectionType.Hospital),
                new SurveyQuestion(8, "What is an opportunity to recommend us to your friends and family?", SurveySectionType.Hospital),
                new SurveyQuestion(9, "How satisfied are you with the services that the hospital provides you?", SurveySectionType.Hospital),
                new SurveyQuestion(10, "What is your overall satisfaction with our hospital?", SurveySectionType.Hospital),

                new SurveyQuestion(11, "How would you rate the kindness of our staff?", SurveySectionType.Staff),
                new SurveyQuestion(12, "How would you rate the professionalism of our staff?", SurveySectionType.Staff),
                new SurveyQuestion(13, "How clearly did the staff explain you some procedures of our hospital?", SurveySectionType.Staff),
                new SurveyQuestion(14, "How yould you rate to what extent staff was available to you during your visit to the hospital?", SurveySectionType.Staff),
                new SurveyQuestion(15, "What is your overall satisfaction with our staff?", SurveySectionType.Staff)
                );

            modelBuilder.Entity<Shift>().HasData(
                    new Shift(1, "Morning shift", 6, 15),
                    new Shift(2, "Afternoon shift", 15, 23),
                    new Shift(3, "Night shift", 23, 6)
                );

           

            modelBuilder.Entity<FloorLabel>().HasData(
               new FloorLabel { id = 1, X = 50, Y = 80, Text = "ENTRANCE", FloorId = 1 }
               );
            //modelbuilder.Entity<Area>().HasData();

            modelBuilder.Entity<Equipment>().HasData(
                new Equipment (1, "Bandage", EquipmentType.DYNAMIC, 15, 15),
                new Equipment (2, "Operating table", EquipmentType.STATIC, 3, 23),
                new Equipment (3, "Infusion", EquipmentType.DYNAMIC, 11, 1),
                new Equipment (4, "Bandage", EquipmentType.DYNAMIC, 17, 2),
                new Equipment (5, "Operating table", EquipmentType.STATIC, 2, 2),
                new Equipment (6, "Infusion", EquipmentType.DYNAMIC, 23, 23),
                new Equipment (7, "Bandage", EquipmentType.DYNAMIC, 15, 3),
                new Equipment (8, "Operating table", EquipmentType.STATIC, 1, 3),
                new Equipment (9, "Syringe", EquipmentType.DYNAMIC, 11, 3),
                new Equipment (10, "Bed", EquipmentType.STATIC, 7, 4),
                new Equipment (11, "Chair", EquipmentType.STATIC, 4, 16),
                new Equipment (12, "Bed", EquipmentType.STATIC, 11, 5),
                new Equipment (13, "Chair", EquipmentType.STATIC, 6, 17),
                new Equipment (14, "Bandage", EquipmentType.DYNAMIC, 25, 5)
                );

            modelBuilder.Entity<Room>().HasData(
                new Room (1, "OPERATING ROOM 1", "", 1, 1, 0, 150, 190, 150, 148, 285, true, "room room-cadetblue", true),
                new Room (2, "OPERATING ROOM 2", "", 2, 1, 160, 150, 100, 150, 220, 248, false, "room room-cadetblue", true),
                new Room (3, "OPERATING ROOM 3","", 3, 1, 320, 150, 100, 150, 370, 248, false, "room room-cadetblue", true),
                new Room (4, "ROOM 1", "", 4, 1, 480, 150, 100, 170, 520, 248, false, "room room-cadetblue", true),
                new Room (5, "ROOM 2", "", 5, 1, 660, 150, 100, 180, 680, 248, false, "room room-cadetblue", true),
                new Room (6, "OFFICE 1","", 6, 1, 730, 260, 100, 110, 728, 290, true, "room room-cadetblue", true ),
                new Room (7, "LIFT","", -1, 1, 690, 370, 90, 150, 728, 290, false, "staircase", false ),
                new Room (8, "TOILET", "", -1, 1, 730, 470, 60, 110, 728, 485, true, "room room-cadetblue", true ),
                new Room (9, "TOILET", "", -1, 1, 730, 540, 60, 110, 728, 555, true, "room room-cadetblue", true ),
                new Room (10, "OPERATING ROOM 4", "", 7, 1, 0, 410, 190, 150, 148, 419, true, "room room-cadetblue", true ),
                new Room (11, "ROOM 3", "", 8, 1, 160, 500, 100, 100, 200, 498, false, "room room-cadetblue", true ),
                new Room (12, "ROOM 4", "",9, 1, 270, 500, 100, 150, 315, 498, false, "room room-cadetblue", true ),
                new Room (13, "ROOM 5", "", 10, 1, 430, 500, 100, 150, 475, 498, false, "room room-cadetblue", true ),
                new Room (14, "DOCTOR'S OFFICE 1", "", 11, 2, 0, 150, 100, 150, 100, 248, false, "room", true ),
                new Room (15, "DOCTOR'S OFFICE 2", "", 12, 2, 160, 150, 100, 150, 260, 248, false, "room", true ),
                new Room (16, "DOCTOR'S OFFICE 3", "", 13, 2, 320, 150, 100, 150, 420, 248, false, "room", true ),
                new Room (17, "DOCTOR'S OFFICE 4", "", 14, 2, 480, 150, 100, 170, 595, 248, false, "room", true ),
                new Room (18, "ROOM 1", "", 15, 2, 660, 150, 100, 180, 680, 248, false, "room", true ),
                new Room (19, "STAIRS", "", -1, 2, 770, 258, 104, 70, 728, 290, true, "room", false ),
                new Room (20, "LIFT", "", -1, 2, 690, 370, 90, 150, 728, 290, false, "staircase", false ),
                new Room (21, "TOILET", "", -1, 2, 730, 470, 60, 110, 728, 485, true, "room", true ),
                new Room (22, "TOILET", "", -1, 2, 730, 540, 60, 110, 728, 555, true, "room", true ),
                new Room (23, "OPERATING ROOM 2", "", 16, 2, 0, 410, 190, 150, 148, 419, true, "room", true ),
                new Room (24, "ROOM 2", "", 17, 2, 160, 500, 100, 100, 200, 498, false, "room", true ),
                new Room (25, "OPERATING ROOM 3", "", 18, 2, 270, 500, 100, 300, 350, 498, false, "room", true),
                new Room (26, "OPERATING ROOM 1", "", 19, 2, 0, 300, 100, 580, 400, 398, false, "room", true)
                );
            modelBuilder.Entity<OutsideDoor>().HasData(
                new OutsideDoor { id = 1, X = 545, Y = 80, IsVertical = true, MapPositionId = 1 },
                new OutsideDoor { id = 2, X = 260, Y = 195, IsVertical = false, MapPositionId = 1 },
                new OutsideDoor { id = 3, X = 545, Y = 505, IsVertical = true, MapPositionId = 2 },
                new OutsideDoor { id = 4, X = 260, Y = 455, IsVertical = false, MapPositionId = 2 });
            //modelbuilder.Entity<Parking>().HasData(new Parking());
            modelBuilder.Entity<MapPosition>().HasData(
                new MapPosition { id = 1, X = 30, Y = 20, Width = 520, Height = 180 },
                new MapPosition { id = 2, X = 30, Y = 460, Width = 520, Height = 180 });
            modelBuilder.Entity<Floor>().HasData(
                new Floor { Id = 1, Level = "Ground floor", BuildingId = 1 },
                new Floor { Id = 2, Level = "First floor", BuildingId = 1 }
                );
            modelBuilder.Entity<Building>().HasData(
                new Building { id = 1, Name = "Hospital I", Area = null, Info = "Gynecology", MapPositionId = 1 },
                new Building { id = 2, Name = "Hospital II", Area = null, Info = "", MapPositionId = 2 });

            modelBuilder.Entity<Doctor>().HasData(
               new Doctor (1,"Slavica", "Matic", 1,-1, 0, false),
               new Doctor (2, "Mirko", "Jankovic", 1, -1, 0, false),
               new Doctor (3, "Matija", "Popic", 2, -1, 0, false),
               new Doctor ( 4, "Sara", "Tot", 1, -1, 0, false),
               new Doctor ( 5, "Ignjat", "Jovic", -1, -1, 0, false),
               new Doctor ( 6, "Milos", "Matijevic", -1, -1, 0, false),
               new Doctor ( 7, "Elena", "Kis", -1, -1, 0, false),
               new Doctor ( 8, "Iva", "Bojanic", -1, -1, 0, false),
               new Doctor ( 9, "Bojan", "Kraljevic", -1, -1, 0, false),
               new Doctor ( 10, "Lidija", "Lakic", -1, -1, 0, false),
               new Doctor ( 11, "Momir", "Njegomir", -1, -1, 0, false),
               new Doctor ( 12, "Ivana", "Pekic", -1, -1, 0, false),
               new Doctor ( 13, "Mileva", "Nakic", -1, -1, 0, false),
               new Doctor ( 14, "Petar", "Katic", -1, -1, 0, false),
               new Doctor ( 15, "Marijana", "Pantic", -1, -1, 0, false),
               new Doctor ( 16, "Savina", "Markovic", -1, -1, 0, false),
               new Doctor ( 17, "Jelena", "Stupar", -1, -1, 0, false),
               new Doctor ( 18, "Luka", "Lisica", -1, -1, 0, false),
               new Doctor ( 19, "Vasilije", "Mit", -1, -1, 0, false)
               );

            modelBuilder.Entity<Vacation>().HasData(
                   new Vacation(1, "aaaa", new DateTime(2022, 1, 21), new DateTime(2022, 1, 31), 1, "Slavica Matic"),
                   new Vacation(2, "aaaa", new DateTime(2022, 2, 21), new DateTime(2022, 2, 25), 2, "Mirko Jankovic"),
                   new Vacation(3, "aaaa", new DateTime(2022, 3, 21), new DateTime(2022, 3, 30), 3, "Matija Popic")
               );

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patients");
                entity.HasKey(c => c.Id);

                entity.OwnsOne(x => x.FullName);
                entity.OwnsOne(x => x.Residence);
            });
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseNpgsql("User ID =admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
                //optionsBuilder.UseNpgsql("User ID =admin;Password=ftn;Server=database;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
                string connectionString = CreateConnectionStringFromEnvironment();
                optionsBuilder.UseNpgsql(connectionString);
            }

        }

        private static string CreateConnectionStringFromEnvironment()
        {
            var server = Environment.GetEnvironmentVariable("DATABASE_HOST") ?? "localhost";
            var port = Environment.GetEnvironmentVariable("DATABASE_PORT") ?? "5432";
            var database = Environment.GetEnvironmentVariable("DATABASE_SCHEMA") ?? "MyWebApi.Dev";
            var user = Environment.GetEnvironmentVariable("DATABASE_USERNAME") ?? "admin";
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD") ?? "ftn";
            var integratedSecurity = Environment.GetEnvironmentVariable("DATABASE_INTEGRATED_SECURITY") ?? "true";
            var pooling = Environment.GetEnvironmentVariable("DATABASE_POOLING") ?? "true";

            //string retVal = "Server=" + server + ";Port=" + port + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";Integrated Security=" + integratedSecurity + ";Pooling=" + pooling + ";";
            return $"Server={server};Port={port};Database={database};User ID={user};Password={password};Integrated Security={integratedSecurity};Pooling={pooling};";
        }
    }
}