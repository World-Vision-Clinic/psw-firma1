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
        public void change_of_dimensions_and_total_number_of_rooms()
        {
            HospitalContext context = GetInMemoryRepository();
                Room r1 = new Room { Id = 88, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
                context.Rooms.Add(r1);
                context.SaveChanges();

                EquipmentRepository eqRepository = new EquipmentRepository(context);
                RoomRepository roomRepository = new RoomRepository(context);
                RoomService roomService = new RoomService(roomRepository, eqRepository);
                Room rr1 = roomRepository.GetByID(88);
                int r1Width = rr1.Width;
                int r1Height = rr1.Height;
                roomService.splitRoom(rr1, "New room 1", "Operations room", "New room 2", "Office");

                Assert.Equal(2,roomRepository.GetAll().Count);
                foreach(Room room in roomRepository.GetAll())
                {
                    if (room.Vertical)
                    {
                        room.Height.ShouldBeLessThan(r1Height);
                    }
                    else
                    {
                        room.Width.ShouldBeLessThan(r1Width);
                    }
                }
                context.Dispose();
            }
        
    }
}
