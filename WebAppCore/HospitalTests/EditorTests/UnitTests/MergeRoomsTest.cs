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
        public void merged_equipment_room_change()
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

                eqRepository.GetRoomEquipemnts(r2.Id).ShouldBeEmpty();
                eqRepository.GetRoomEquipemnts(r1.Id).ShouldBeEmpty();
                eqRepository.GetRoomEquipemnts(newId).Count.ShouldBeEquivalentTo(4);
                context.Dispose();
            }
        }
        [Fact]
        public void same_name_equipment_merging()
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
                Equipment eq1 = new Equipment { Id = 717, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 15, RoomId = 399 };
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
                foreach(Equipment e in eqRepository.GetAll())
                {
                    if (e.Name.Equals("Bandage"))
                    {
                        e.Amount.ShouldBeEquivalentTo(19);
                    }
                }
               
                context.Dispose();
            }
        }

        [Fact]
        public void changin_total_number_of_roms_and_dimensions()
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
                List<Room> newRooms = roomService.getAll();

                roomRepository.GetAll().Count.ShouldBeEquivalentTo(1);

                if (r1.Vertical)
                {
                    (r1.Height + r2.Height).ShouldBeLessThanOrEqualTo(newRooms[0].Height);
                }
                else
                {
                    (r1.Width + r2.Width).ShouldBeLessThanOrEqualTo(newRooms[0].Width);
                }
                

                context.Dispose();
            }
        }
    }
}
