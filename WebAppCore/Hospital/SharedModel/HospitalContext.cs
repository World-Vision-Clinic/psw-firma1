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
        public DbSet<OnCallShift> OnCallShifts { get; set; }
        public DbSet<Renovation> Renovations { get; set; }
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

            modelBuilder.Entity<OnCallShift>(entity =>
            {
                entity.ToTable("OnCallShifts");
                entity.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Renovation>().HasData(
                new Renovation { NewRoomName1 = "Test 123", NewRoomName2 = "", NewRoomPurpose1 = "123", NewRoomPurpose2 = "", Room1Id = 4, Room2Id = 5, StartDate = new DateTime(2021, 12, 20, 8, 0, 0, 0, DateTimeKind.Local), EndDate = new DateTime(2021, 12, 27, 8, 0, 0, 0, DateTimeKind.Local), isMerge = true, id = 1 });

            modelBuilder.Entity<Appointment>().HasData(
                // new Appointment { Id = 1, Surveys = new List<Survey>() }
                new Appointment { Id = 1, PatientForeignKey = 1, DoctorForeignKey = 1, Type = (AppointmentType)1, Date = new DateTime(2022, 1, 13), Time = new TimeSpan(14, 10, 0), IsCancelled = false, RoomId = 1 },
                new Appointment { Id = 2, PatientForeignKey = 2, DoctorForeignKey = 1, Type = (AppointmentType)1, Date = new DateTime(2022, 1, 17), Time = new TimeSpan(16, 30, 0), IsCancelled = false, RoomId = 1 },
                new Appointment { Id = 3, PatientForeignKey = 1, DoctorForeignKey = 2, Type = (AppointmentType)2, Date = new DateTime(2022, 2, 13), Time = new TimeSpan(10, 10, 0), IsCancelled = false, RoomId = 2 },
                new Appointment { Id = 4, PatientForeignKey = 3, DoctorForeignKey = 13, Type = (AppointmentType)2, Date = new DateTime(2022, 1, 10), Time = new TimeSpan(11, 15, 0), IsCancelled = false, RoomId = 13 },
                new Appointment { Id = 5, PatientForeignKey = 1, DoctorForeignKey = 3, Type = (AppointmentType)1, Date = new DateTime(2021, 12, 30), Time = new TimeSpan(14, 30, 0), IsCancelled = false, RoomId = 3 },
                new Appointment { Id = 6, PatientForeignKey = 2, DoctorForeignKey = 4, Type = (AppointmentType)1, Date = new DateTime(2022, 1, 14), Time = new TimeSpan(17, 00, 0), IsCancelled = false, RoomId = 4 },
                new Appointment { Id = 7, PatientForeignKey = 1, DoctorForeignKey = 3, Type = (AppointmentType)1, Date = new DateTime(2021, 12, 29), Time = new TimeSpan(17, 30, 0), IsCancelled = false, RoomId = 3 },
                new Appointment { Id = 8, PatientForeignKey = 2, DoctorForeignKey = 6, Type = (AppointmentType)1, Date = new DateTime(2022, 3, 14), Time = new TimeSpan(13, 00, 0), IsCancelled = false, RoomId = 6 }
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
                new Equipment
                {
                    Id = 1,
                    Name = "Bandage",
                    Type = EquipmentType.DYNAMIC,
                    Amount = 15,
                    RoomId = 15,
                    InTransport = true,
                    TransportStart = new DateTime(2021, 12, 14, 10, 0, 0, 0, DateTimeKind.Unspecified),
                    TransportEnd = new DateTime(2021, 12, 14, 11, 0, 0, 0, DateTimeKind.Unspecified)
                },
                new Equipment
                {
                    Id = 2,
                    Name = "Operating table",
                    Type = EquipmentType.STATIC,
                    Amount = 3,
                    RoomId = 23,
                    InTransport = true,
                    TransportStart = new DateTime(2021, 12, 15, 11, 0, 0, 0, DateTimeKind.Unspecified),
                    TransportEnd = new DateTime(2021, 12, 15, 12, 0, 0, 0, DateTimeKind.Unspecified)
                },
                new Equipment
                {
                    Id = 3,
                    Name = "Infusion",
                    Type = EquipmentType.DYNAMIC,
                    Amount = 11,
                    RoomId = 1,
                    InTransport = true,
                    TransportStart = new DateTime(2021, 12, 16, 10, 0, 0, 0, DateTimeKind.Unspecified),
                    TransportEnd = new DateTime(2021, 12, 16, 11, 0, 0, 0, DateTimeKind.Unspecified)
                },
                new Equipment { Id = 4, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 17, RoomId = 2,
                    InTransport = true,
                    TransportStart = new DateTime(2022, 1, 14, 10, 0, 0, 0, DateTimeKind.Unspecified),
                    TransportEnd = new DateTime(2022, 1, 14, 11, 0, 0, 0, DateTimeKind.Unspecified)
                },
                new Equipment { Id = 5, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 2, RoomId = 2,
                    InTransport = true,
                    TransportStart = new DateTime(2022, 1, 17, 13, 0, 0, 0, DateTimeKind.Unspecified),
                    TransportEnd = new DateTime(2022, 1, 17, 14, 0, 0, 0, DateTimeKind.Unspecified)
                },
                new Equipment { Id = 6, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 23 },
                new Equipment { Id = 7, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 3 },
                new Equipment { Id = 8, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 1, RoomId = 3 },
                new Equipment { Id = 9, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 11, RoomId = 3 },
                new Equipment { Id = 10, Name = "Bed", Type = EquipmentType.STATIC, Amount = 7, RoomId = 4 },
                new Equipment { Id = 11, Name = "Chair", Type = EquipmentType.STATIC, Amount = 4, RoomId = 16 },
                new Equipment { Id = 12, Name = "Bed", Type = EquipmentType.STATIC, Amount = 11, RoomId = 5 },
                new Equipment { Id = 13, Name = "Chair", Type = EquipmentType.STATIC, Amount = 6, RoomId = 17 },
                new Equipment { Id = 14, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 25, RoomId = 5 }
                );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = 1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 2, FloorId = 1, Name = "OPERATING ROOM 2", DoctorId = 2, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 220, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 3, FloorId = 1, Name = "OPERATING ROOM 3", DoctorId = 3, Purpose = "", X = 320, Y = 150, Height = 100, Width = 150, DoorX = 370, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 4, FloorId = 1, Name = "ROOM 1", DoctorId = 4, Purpose = "", X = 480, Y = 150, Height = 100, Width = 170, DoorX = 520, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 5, FloorId = 1, Name = "ROOM 2", DoctorId = 5, Purpose = "", X = 660, Y = 150, Height = 100, Width = 180, DoorX = 680, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 6, FloorId = 1, Name = "OFFICE 1", DoctorId = 6, Purpose = "", X = 730, Y = 260, Height = 100, Width = 110, DoorX = 728, DoorY = 290, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 7, FloorId = 1, Name = "LIFT", DoctorId = -1, Purpose = "", X = 690, Y = 370, Height = 90, Width = 150, DoorX = 728, DoorY = 290, Vertical = false, Css = "staircase", DoorExist = false },
                new Room { Id = 8, FloorId = 1, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 470, Height = 60, Width = 110, DoorX = 728, DoorY = 485, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 9, FloorId = 1, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 540, Height = 60, Width = 110, DoorX = 728, DoorY = 555, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 10, FloorId = 1, Name = "OPERATING ROOM 4", DoctorId = 7, Purpose = "", X = 0, Y = 410, Height = 190, Width = 150, DoorX = 148, DoorY = 419, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 11, FloorId = 1, Name = "ROOM 3", DoctorId = 8, Purpose = "", X = 160, Y = 500, Height = 100, Width = 100, DoorX = 200, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 12, FloorId = 1, Name = "ROOM 4", DoctorId = 9, Purpose = "", X = 270, Y = 500, Height = 100, Width = 150, DoorX = 315, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 13, FloorId = 1, Name = "ROOM 5", DoctorId = 10, Purpose = "", X = 430, Y = 500, Height = 100, Width = 150, DoorX = 475, DoorY = 498, Vertical = false, Css = "room room-cadetblue", DoorExist = true },
                new Room { Id = 14, FloorId = 2, Name = "DOCTOR'S OFFICE 1", DoctorId = 11, Purpose = "", X = 0, Y = 150, Height = 100, Width = 150, DoorX = 100, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 15, FloorId = 2, Name = "DOCTOR'S OFFICE 2", DoctorId = 12, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 260, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 16, FloorId = 2, Name = "DOCTOR'S OFFICE 3", DoctorId = 13, Purpose = "", X = 320, Y = 150, Height = 100, Width = 150, DoorX = 420, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 17, FloorId = 2, Name = "DOCTOR'S OFFICE 4", DoctorId = 14, Purpose = "", X = 480, Y = 150, Height = 100, Width = 170, DoorX = 595, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 18, FloorId = 2, Name = "ROOM 1", DoctorId = 15, Purpose = "", X = 660, Y = 150, Height = 100, Width = 180, DoorX = 680, DoorY = 248, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 19, FloorId = 2, Name = "STAIRS", DoctorId = -1, Purpose = "", X = 770, Y = 258, Height = 104, Width = 70, DoorX = 728, DoorY = 290, Vertical = true, Css = "room", DoorExist = false },
                new Room { Id = 20, FloorId = 2, Name = "LIFT", DoctorId = -1, Purpose = "", X = 690, Y = 370, Height = 90, Width = 150, DoorX = 728, DoorY = 290, Vertical = false, Css = "staircase", DoorExist = false },
                new Room { Id = 21, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 470, Height = 60, Width = 110, DoorX = 728, DoorY = 485, Vertical = true, Css = "room", DoorExist = true },
                new Room { Id = 22, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 540, Height = 60, Width = 110, DoorX = 728, DoorY = 555, Vertical = true, Css = "room", DoorExist = true },
                new Room { Id = 23, FloorId = 2, Name = "OPERATING ROOM 2", DoctorId = 16, Purpose = "", X = 0, Y = 410, Height = 190, Width = 150, DoorX = 148, DoorY = 419, Vertical = true, Css = "room", DoorExist = true },
                new Room { Id = 24, FloorId = 2, Name = "ROOM 2", DoctorId = 17, Purpose = "", X = 160, Y = 500, Height = 100, Width = 100, DoorX = 200, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 25, FloorId = 2, Name = "OPERATING ROOM 3", DoctorId = 18, Purpose = "", X = 270, Y = 500, Height = 100, Width = 300, DoorX = 350, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { Id = 26, FloorId = 2, Name = "OPERATING ROOM 1", DoctorId = 19, Purpose = "", X = 0, Y = 300, Height = 100, Width = 580, DoorX = 400, DoorY = 398, Vertical = false, Css = "room", DoorExist = true }
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