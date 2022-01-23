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
            builder.UseInMemoryDatabase("TestDbase");
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
                Room r1 = new Room (88, "OPERATING ROOM 1","",-1, 1, 0, 150, 190, 150, 148, 285, true, "room room-cadetblue", true );
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
