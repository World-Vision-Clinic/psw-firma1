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
    public class SplitRoomTest
    {
        [Fact]
        public void split_room_test_1()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;

            using (var context = new HospitalContext(options))
            {
                Room r1 = new Room { Id = 88, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
                context.Rooms.Add(r1);


                context.SaveChanges();
            }
            using (var context = new HospitalContext(options))
            {
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room r1 = roomRepository.GetByID(88);
                int r1Width = r1.Width;
                int r1Height = r1.Height;
                roomService.splitRoom(r1, "New room 1", "Operations room", "New room 2", "Office");

                roomRepository.GetAll().Count.ShouldBe(2);
                foreach(Room room in roomRepository.GetAll())
                {
                    if (room.Vertical)
                    {
                        room.Height.ShouldBeLessThan(r1Height);
                    }
                    else
                    {
                        room.Width.ShouldBeLessThan(r1.Width);
                    }
                }

                context.Dispose();
            }
            }
        
    }
}
