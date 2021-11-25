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
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Allergen> Allergens { get; set; }
        public DbSet<PatientAllergen> PatientAllergens { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<OutsideDoor> OutsideDoors { get; set; }
        public DbSet<MapPosition> MapPositions { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<FloorLabel> FloorLabels { get; set; }
        public DbSet<Equipment> AllEquipment { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Building> Buildings { get; set; }


        public HospitalContext() { }

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

            modelBuilder.Entity<FloorLabel>().HasData(
               new FloorLabel { id = 1, X = 50, Y = 80, Text = "ENTRANCE", FloorId = 1 }
               );
            //modelbuilder.Entity<Area>().HasData();
            modelBuilder.Entity<Equipment>().HasData(
                new Equipment { id = 1, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 15 },
                new Equipment { id = 2, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 3, RoomId = 23 },
                new Equipment { id = 3, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 11, RoomId = 1 },
                new Equipment { id = 4, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 17, RoomId = 2 },
                new Equipment { id = 5, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 2, RoomId = 2 },
                new Equipment { id = 6, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 23 },
                new Equipment { id = 7, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 3 },
                new Equipment { id = 8, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 1, RoomId = 3 },
                new Equipment { id = 9, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 11, RoomId = 3 },
                new Equipment { id = 10, Name = "Bed", Type = EquipmentType.STATIC, Amount = 7, RoomId = 4 },
                new Equipment { id = 11, Name = "Chair", Type = EquipmentType.STATIC, Amount = 4, RoomId = 16 },
                new Equipment { id = 12, Name = "Bed", Type = EquipmentType.STATIC, Amount = 11, RoomId = 5 },
                new Equipment { id = 13, Name = "Chair", Type = EquipmentType.STATIC, Amount = 6, RoomId = 17 },
                new Equipment { id = 14, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 25, RoomId = 5 }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room { id = 1, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 2, FloorId = 1, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 220, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 3, FloorId = 1, Name = "OPERATING ROOM 3", DoctorId = -1, Purpose = "", X = 320, Y = 150, Height = 100, Width = 150, DoorX = 370, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 4, FloorId = 1, Name = "ROOM 1", DoctorId = -1, Purpose = "", X = 480, Y = 150, Height = 100, Width = 170, DoorX = 520, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 5, FloorId = 1, Name = "ROOM 2", DoctorId = -1, Purpose = "", X = 660, Y = 150, Height = 100, Width = 180, DoorX = 680, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 6, FloorId = 1, Name = "OFFICE 1", DoctorId = -1, Purpose = "", X = 730, Y = 260, Height = 100, Width = 110, DoorX = 728, DoorY = 290, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 7, FloorId = 1, Name = "LIFT", DoctorId = -1, Purpose = "", X = 690, Y = 370, Height = 90, Width = 150, DoorX = 728, DoorY = 290, Vertical = false, Css = "staircase", DoorExist = false },
                new Room { id = 8, FloorId = 1, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 470, Height = 60, Width = 110, DoorX = 728, DoorY = 485, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 9, FloorId = 1, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 540, Height = 60, Width = 110, DoorX = 728, DoorY = 555, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 10, FloorId = 1, Name = "OPERATING ROOM 4", DoctorId = -1, Purpose = "", X = 0, Y = 410, Height = 190, Width = 150, DoorX = 148, DoorY = 419, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 11, FloorId = 1, Name = "ROOM 3", DoctorId = -1, Purpose = "", X = 160, Y = 500, Height = 100, Width = 100, DoorX = 200, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 12, FloorId = 1, Name = "ROOM 4", DoctorId = -1, Purpose = "", X = 270, Y = 500, Height = 100, Width = 150, DoorX = 315, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 13, FloorId = 1, Name = "ROOM 5", DoctorId = -1, Purpose = "", X = 430, Y = 500, Height = 100, Width = 150, DoorX = 475, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { id = 14, FloorId = 2, Name = "DOCTOR'S OFFICE 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 100, Width = 150, DoorX = 100, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 15, FloorId = 2, Name = "DOCTOR'S OFFICE 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 260, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 16, FloorId = 2, Name = "DOCTOR'S OFFICE 3", DoctorId = -1, Purpose = "", X = 320, Y = 150, Height = 100, Width = 150, DoorX = 420, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 17, FloorId = 2, Name = "DOCTOR'S OFFICE 4", DoctorId = -1, Purpose = "", X = 480, Y = 150, Height = 100, Width = 170, DoorX = 595, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 18, FloorId = 2, Name = "ROOM 1", DoctorId = -1, Purpose = "", X = 660, Y = 150, Height = 100, Width = 180, DoorX = 680, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 19, FloorId = 2, Name = "STAIRS", DoctorId = -1, Purpose = "", X = 770, Y = 258, Height = 104, Width = 70, DoorX = 728, DoorY = 290, Vertical = true, Css = "room", DoorExist = false },
                new Room { id = 20, FloorId = 2, Name = "LIFT", DoctorId = -1, Purpose = "", X = 690, Y = 370, Height = 90, Width = 150, DoorX = 728, DoorY = 290, Vertical = false, Css = "staircase", DoorExist = false },
                new Room { id = 21, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 470, Height = 60, Width = 110, DoorX = 728, DoorY = 485, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 22, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 540, Height = 60, Width = 110, DoorX = 728, DoorY = 555, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 23, FloorId = 2, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 0, Y = 410, Height = 190, Width = 150, DoorX = 148, DoorY = 419, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 24, FloorId = 2, Name = "ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 500, Height = 100, Width = 100, DoorX = 200, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 25, FloorId = 2, Name = "OPERATING ROOM 3", DoctorId = -1, Purpose = "", X = 270, Y = 500, Height = 100, Width = 300, DoorX = 350, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 26, FloorId = 2, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 300, Height = 100, Width = 580, DoorX = 400, DoorY = 398, Vertical = false, Css = "room", DoorExist = true }
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
                new Floor { id = 1, Level = "Ground floor", BuildingId = 1 },
                new Floor { id = 2, Level = "First floor", BuildingId = 1 }
                );
            modelBuilder.Entity<Building>().HasData(
                new Building { id = 1, Name = "Hospital I", Area = null, Info = "Gynecology", MapPositionId = 1 },
                new Building { id = 2, Name = "Hospital II", Area = null, Info = "", MapPositionId = 2 });

        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
            }
        }
    }
}