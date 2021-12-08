using Hospital.RepositoryInterfaces;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.EditorTests.UnitTests
{
    public class MergeRoomsTest
    {
        [Fact]
        public void merge_equipment_test_1()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;

            using (var context = new HospitalContext(options))
            {
                Room r1 = new Room { Id = 88, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
                Room r2 = new Room { Id = 888, FloorId = 1, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 220, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true };
                context.Rooms.Add(r1);
                context.Rooms.Add(r2);
                Equipment eq1 = new Equipment { Id = 871, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 88 };
                Equipment eq2 = new Equipment { Id = 872, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 4, RoomId = 888 };
                Equipment eq3 = new Equipment { Id = 873, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 7, RoomId = 88 };
                Equipment eq4 = new Equipment { Id = 874, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 888 };
                context.AllEquipment.Add(eq1);
                context.AllEquipment.Add(eq2);
                context.AllEquipment.Add(eq3);
                context.AllEquipment.Add(eq4);

                context.SaveChanges();
            }
            using (var context = new HospitalContext(options))
            {
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room r1 = roomRepository.GetByID(88);
                Room r2 = roomRepository.GetByID(888);
                int newId = 699;

                roomService.mergeEquipment(r1.Id, r2.Id, newId);

                Assert.Empty(eqRepository.GetRoomEquipemnts(r2.Id));
                context.Dispose();
            }
        }
        [Fact]
        public void merge_equipment_test_2()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;

            using (var context = new HospitalContext(options))
            {

                Room r1 = new Room { Id = 399, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
                Room r2 = new Room { Id = 3999, FloorId = 1, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 220, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true };
                context.Rooms.Add(r1);
                context.Rooms.Add(r2);
                Equipment eq1 = new Equipment { Id = 737, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 399 };
                Equipment eq2 = new Equipment { Id = 738, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 4, RoomId = 3999 };
                Equipment eq3 = new Equipment { Id = 733, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 7, RoomId = 399 };
                Equipment eq4 = new Equipment { Id = 734, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 3999 };
                context.AllEquipment.Add(eq1);
                context.AllEquipment.Add(eq2);
                context.AllEquipment.Add(eq3);
                context.AllEquipment.Add(eq4);

                context.SaveChanges();
            }
            using (var context = new HospitalContext(options))
            {
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room r1 = roomRepository.GetByID(399);
                Room r2 = roomRepository.GetByID(3999);
                int newId = 3499;

                roomService.mergeEquipment(r1.Id, r2.Id, newId);

                Assert.Equal(4, eqRepository.GetRoomEquipemnts(newId).Count);
                context.Dispose();
            }
        }

        [Fact]
        public void merge_rooms_test()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;
            using (var context = new HospitalContext(options))
            {

                Room r1 = new Room { Id = 399, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
                Room r2 = new Room { Id = 3999, FloorId = 1, Name = "OPERATING ROOM 2", DoctorId = -1, Purpose = "", X = 160, Y = 150, Height = 100, Width = 150, DoorX = 220, DoorY = 248, Vertical = false, Css = "room room-cadetblue", DoorExist = true };
                context.Rooms.Add(r1);
                context.Rooms.Add(r2);
                Equipment eq1 = new Equipment { Id = 737, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 399 };
                Equipment eq2 = new Equipment { Id = 738, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 4, RoomId = 3999 };
                Equipment eq3 = new Equipment { Id = 733, Name = "Infusion", Type = EquipmentType.DYNAMIC, Amount = 7, RoomId = 399 };
                Equipment eq4 = new Equipment { Id = 734, Name = "Syringe", Type = EquipmentType.DYNAMIC, Amount = 23, RoomId = 3999 };
                context.AllEquipment.Add(eq1);
                context.AllEquipment.Add(eq2);
                context.AllEquipment.Add(eq3);
                context.AllEquipment.Add(eq4);

                context.SaveChanges();
            }
            using (var context = new HospitalContext(options))
            {
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room r1 = roomRepository.GetByID(399);
                Room r2 = roomRepository.GetByID(3999);


                roomService.mergeRooms(r1, r2, "New Room", "Room for personel rest");


                Assert.Equal(1,roomRepository.GetAll().Count);

                context.Dispose();
            }
        }
    }
}
