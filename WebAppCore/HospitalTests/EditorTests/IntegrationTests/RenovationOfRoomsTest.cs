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
using Hospital_API.Controllers;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Hospital_API;
using Hospital_API.DTO;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RenovationOfRoomsTest
    {
        public int roomId;

        [Fact]
        public void merge_integration_test()
        {
            //Arrange
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new EquipmentRepository(new HospitalContext()));
            Room r1 = service.GetById(8);
            Room r2 = service.GetById(9);
            int numOfRooms = service.getAll().Count;
            //Act
            roomId = service.mergeRooms(r1, r2, "MERGED ROOM 1", "For doctors rest.");
           
            //Assert
            service.getAll().Count.ShouldBe(numOfRooms - 1);
            service.GetById(roomId).FloorId.ShouldBeEquivalentTo(r1.FloorId);
        }
        
        [Fact]
        public void split_integration_test()
        {
            //Arrange
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new EquipmentRepository(new HospitalContext()));
            Room room = service.GetById(10);
            int numOfRooms = service.getAll().Count;

            //Act
            service.splitRoom(room, "ROOM 4", "something", "ROOM 5", "something");

            //Assert
            service.getAll().Count.ShouldBe(numOfRooms + 1);
        }
    }
}
