using System;
using Xunit;
using Hospital.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using Hospital.SharedModel;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital_API.Controllers;
using Hospital_API.DTO;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RoomControllerTest
    {
        public IRoomRepository inMemoryRepo;

        public RoomControllerTest()
        {
        }

        private IRoomRepository GetInMemoryRoomRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            TestContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();

            return new RoomRepository(hospitalContext);
        }

        [Fact]
        public void Test_room_found()
        {   //Arrange
            inMemoryRepo = GetInMemoryRoomRepository();

            Room room = new Room { Id = 1, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
            //Act
            inMemoryRepo.Save(room);
            var controller = new RoomsController(inMemoryRepo);
            var response = controller.GetRoom(1);
            //Assert
            Assert.Equal(1, response.Value.id);
        }

        [Fact]
        public void Test_not_room_found()
        {   //Arrange
            inMemoryRepo = GetInMemoryRoomRepository();

            Room room = new Room { Id = 1, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
            //Act
            inMemoryRepo.Save(room);
            var controller = new RoomsController(inMemoryRepo);
            var response = controller.GetRoom(2);
            //Assert
            Assert.NotEqual(1, response.Value.id);
        }

        [Fact]
        public void Test_room_update()
        {   //Arrange
            inMemoryRepo = GetInMemoryRoomRepository();

            Room room = new Room { Id = 1, FloorId = 1, Name = "OPERATING ROOM 1", DoctorId = -1, Purpose = "", X = 0, Y = 150, Height = 190, Width = 150, DoorX = 148, DoorY = 285, Vertical = true, Css = "room room-cadetblue", DoorExist = true };
            RoomDTO roomDto = new RoomDTO(room);
            //Act
            inMemoryRepo.Save(room);
            var controller = new RoomsController(inMemoryRepo);
            roomDto.name = "Operating Room 1";
            controller.PutRooms(1, roomDto);
            var response = controller.GetRoom(1);
            //Assert
            Assert.Equal("Operating Room 1", response.Value.name);
        }
    }
}
