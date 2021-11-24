using Hospital;
using Hospital.MedicalRecords.Model;
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
        public DbSet<Feedback> Feedbacks{ get; set; }
        public DbSet<Patient> Patients { get; set; }
        
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

        public HospitalContext(DbContextOptions<TestContext> options) : base(options) { }
        public DbSet<Area> Areas { get; set; }
        public DbSet<OutsideDoor> OutsideDoors { get; set; }
        public DbSet<MapPosition> MapPositions { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<FloorLabel> FloorLabels { get; set; }
        public DbSet<Equipment> AllEquipment { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<Building> Buildings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<FloorLabel>().HasData(
               new FloorLabel { id = 1, X = 50, Y = 80, Text = "ENTRANCE", FloorId = 1 }
               );
            //modelbuilder.Entity<Area>().HasData();
            modelbuilder.Entity<Equipment>().HasData(
                new Equipment { id = 1, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 1 },
                new Equipment { id = 2, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 3, RoomId = 1 },
                new Equipment { id = 3, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 11, RoomId = 1 },
                new Equipment { id = 4, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 17, RoomId = 2 },
                new Equipment { id = 5, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 2, RoomId = 2 },
                new Equipment { id = 6, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 2 },
                new Equipment { id = 7, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 3 },
                new Equipment { id = 8, Name = "Operating table", Type = EquipmentType.STATIC, Amount = 1, RoomId = 3 },
                new Equipment { id = 9, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 11, RoomId = 3 },
                new Equipment { id = 10, Name = "Bed", Type = EquipmentType.STATIC, Amount = 7, RoomId = 4 },
                new Equipment { id = 11, Name = "Chair", Type = EquipmentType.STATIC, Amount = 4, RoomId = 4 },
                new Equipment { id = 12, Name = "Bed", Type = EquipmentType.STATIC, Amount = 11, RoomId = 5 },
                new Equipment { id = 13, Name = "Chair", Type = EquipmentType.STATIC, Amount = 6, RoomId = 5 },
                new Equipment { id = 14, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 25, RoomId = 5 }
                );
           
            modelbuilder.Entity<Room>().HasData(
                new Room { id = 1, FloorId=1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true },
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
                new Room { id = 15, FloorId = 2, Name = "DOCTOR'S OFFICE 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 260, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 16, FloorId = 2, Name = "DOCTOR'S OFFICE 3", DoctorId = -1, Purpose = "", X = 320, Y = 150, Height = 100, Width = 150, DoorX = 420, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 17, FloorId = 2, Name = "DOCTOR'S OFFICE 4", DoctorId = -1, Purpose = "", X = 480, Y = 150, Height = 100, Width = 170, DoorX = 595, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 18, FloorId = 2, Name = "ROOM 1", DoctorId = -1, Purpose = "", X = 660, Y = 150, Height = 100, Width = 180, DoorX = 680, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 19, FloorId = 2, Name = "STAIRS", DoctorId = -1, Purpose = "", X = 770, Y = 258, Height = 104, Width = 70, DoorX = 728, DoorY = 290, Vertical = true, Css = "room", DoorExist = false },
                new Room { id = 20, FloorId = 2, Name = "LIFT", DoctorId = -1, Purpose = "", X = 690, Y = 370, Height = 90, Width = 150, DoorX = 728, DoorY = 290, Vertical = false, Css = "staircase", DoorExist = false },
                new Room { id = 21, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 470, Height = 60, Width = 110, DoorX = 728, DoorY = 485, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 22, FloorId = 2, Name = "TOILET", DoctorId = -1, Purpose = "", X = 730, Y = 540, Height = 60, Width = 110, DoorX = 728, DoorY = 555, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 23, FloorId = 2, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 0, Y = 410, Height = 190, Width = 150, DoorX = 148, DoorY = 419, Vertical = true, Css = "room", DoorExist = true },
                new Room { id = 24, FloorId = 2, Name = "ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 500, Height = 100, Width = 100, DoorX = 200, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 25, FloorId = 2, Name = "OPERATING ROOM 3", DoctorId = -1, Purpose = "", X = 270, Y = 500, Height = 100, Width = 300, DoorX = 350, DoorY = 498, Vertical = false, Css = "room", DoorExist = true },
                new Room { id = 26, FloorId = 2, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 300, Height = 100, Width = 580, DoorX = 400, DoorY = 398, Vertical = false, Css = "room", DoorExist = true }
                );
            modelbuilder.Entity<OutsideDoor>().HasData(
                new OutsideDoor { id = 1, X = 545, Y = 80, IsVertical = true, MapPositionId = 1 },
                new OutsideDoor { id = 2, X = 260, Y = 195, IsVertical = false, MapPositionId = 1 },
                new OutsideDoor { id = 3, X = 545, Y = 505, IsVertical = true, MapPositionId = 2 },
                new OutsideDoor { id = 4, X = 260, Y = 455, IsVertical = false, MapPositionId = 2 });
            //modelbuilder.Entity<Parking>().HasData(new Parking());
            modelbuilder.Entity<MapPosition>().HasData(
                new MapPosition { id = 1, X = 30, Y = 20, Width = 520, Height = 180 },
                new MapPosition { id = 2, X = 30, Y = 460, Width = 520, Height = 180 });
            modelbuilder.Entity<Floor>().HasData(
                new Floor { id = 1, Level = "Ground floor", BuildingId = 1 },
                new Floor { id = 2, Level = "First floor", BuildingId = 1 }
                );
            modelbuilder.Entity<Building>().HasData(
                new Building { id = 1, Name = "Hospital I", Area = null, Info = "Gynecology", MapPositionId = 1 },
                new Building { id = 2, Name = "Hospital II", Area = null, Info = "", MapPositionId = 2 });
        }

        protected override void OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=localhost;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
                //optionsBuilder.UseNpgsql("User ID = admin;Password=ftn;Server=hospitalapi;Port=5432;Database=MyWebApi.Dev;Integrated Security=true;Pooling=true;");
            }
        }
    }
}
