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
        private HospitalContext GetInMemoryRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            HospitalContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            return hospitalContext;
        }

        [Fact]
        public void merged_equipment_room_change()
        {
            HospitalContext context = GetInMemoryRepository();

            Room r1 = new Room (88, "OPERATING ROOM 1", "", -1, 1, 0, 150, 190, 150, 148, 285, true, "room room-cadetblue", true);
            Room r2 = new Room (888, "OPERATING ROOM 2", "",-1, 1, 160, 150, 100, 150, 220, 248, false, "room room-cadetblue", true );
            context.Rooms.Add(r1);
            context.Rooms.Add(r2);
            Equipment eq1 = new Equipment(871, "Bandage", EquipmentType.DYNAMIC, 15, 88);
            Equipment eq2 = new Equipment(872, "Bandage", EquipmentType.DYNAMIC, 4, 888);
            Equipment eq3 = new Equipment(873, "Infusion", EquipmentType.DYNAMIC, 7, 88);
            Equipment eq4 = new Equipment(874, "Syringe", EquipmentType.DYNAMIC, 23, 888);
            context.AllEquipment.Add(eq1);
            context.AllEquipment.Add(eq2);
            context.AllEquipment.Add(eq3);
            context.AllEquipment.Add(eq4);
            context.SaveChanges();

            EquipmentRepository eqRepository = new EquipmentRepository(context);
            RoomRepository roomRepository = new RoomRepository(context);
            RoomService roomService = new RoomService(roomRepository, eqRepository);

            Room rr1 = roomRepository.GetByID(88);
            Room rr2 = roomRepository.GetByID(888);
            int newId = 699;

            roomService.mergeEquipment(rr1.Id, rr2.Id, newId);

            Assert.Empty(eqRepository.GetRoomEquipemnts(rr2.Id));
            Assert.Empty(eqRepository.GetRoomEquipemnts(rr1.Id));

            context.Dispose();
        }
        
        [Fact]
        public void same_name_equipment_merging()
        {
            HospitalContext context = GetInMemoryRepository();

                Room r1 = new Room(399, "OPERATING ROOM 1", "", -1, 1, 0, 150, 190, 150, 148, 285, true, "room room-cadetblue", true);
                Room r2 = new Room(3999, "OPERATING ROOM 2", "", -1, 1, 160, 150, 100, 150, 220, 248, false, "room room-cadetblue", true);
                context.Rooms.Add(r1);
                context.Rooms.Add(r2);
                Equipment eq1 = new Equipment(717, "Bandage", EquipmentType.DYNAMIC, 15, 399);
                Equipment eq2 = new Equipment(738, "Bandage", EquipmentType.DYNAMIC, 4, 3999);
                Equipment eq3 = new Equipment(733, "Infusion", EquipmentType.DYNAMIC, 7, 399);
                Equipment eq4 = new Equipment(734, "Syringe", EquipmentType.DYNAMIC, 23, 3999);
                context.AllEquipment.Add(eq1);
                context.AllEquipment.Add(eq2);
                context.AllEquipment.Add(eq3);
                context.AllEquipment.Add(eq4);

                context.SaveChanges();
                      
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room rr1 = roomRepository.GetByID(399);
                Room rr2 = roomRepository.GetByID(3999);
                int newId = 3499;

                roomService.mergeEquipment(rr1.Id, rr2.Id, newId);
            List<Equipment> list = eqRepository.GetAll();
                foreach(Equipment e in eqRepository.GetAll())
                {
                    if (e.Name.Equals("Bandage"))
                    {
                        e.Amount.ShouldBeEquivalentTo(19);
                    }
                }
               
                context.Dispose();
        }

        [Fact]
        public void changin_total_number_of_roms_and_dimensions()
        {
            HospitalContext context = GetInMemoryRepository();
            Room r1 = new Room(399, "OPERATING ROOM 1", "", -1, 1, 0, 150, 190, 150, 148, 285, true, "room room-cadetblue", true);
            Room r2 = new Room(3999, "OPERATING ROOM 2", "", -1, 1, 160, 150, 100, 150, 220, 248, false, "room room-cadetblue", true);
            context.Rooms.Add(r1);
                context.Rooms.Add(r2);
                Equipment eq1 = new Equipment ( 7379, "Bandage", EquipmentType.DYNAMIC, 15, 399 );
                Equipment eq2 = new Equipment ( 7389, "Bandage", EquipmentType.DYNAMIC, 4, 3999 );
                Equipment eq3 = new Equipment ( 7339, "Infusion", EquipmentType.DYNAMIC, 7, 399 );
                Equipment eq4 = new Equipment ( 7349, "Syringe", EquipmentType.DYNAMIC, 23, 3999 );
                context.AllEquipment.Add(eq1);
                context.AllEquipment.Add(eq2);
                context.AllEquipment.Add(eq3);
                context.AllEquipment.Add(eq4);

                context.SaveChanges();

                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room rr1 = roomRepository.GetByID(399);
                Room rr2 = roomRepository.GetByID(3999);


                roomService.mergeRooms(rr1, rr2, "New Room", "Room for personel rest");
                List<Room> newRooms = roomService.getAll();

               /* roomRepository.GetAll().Count.ShouldBeEquivalentTo(1);

                if (r1.Vertical)
                {
                    (r1.Height + r2.Height).ShouldBeLessThanOrEqualTo(newRooms[0].Height);
                }
                else
                {
                    (r1.Width + r2.Width).ShouldBeLessThanOrEqualTo(newRooms[0].Width);
                }*/
                

                context.Dispose();           
        }
    }
}
