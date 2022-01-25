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
using Hospital.Schedule.Service;
using Hospital.Schedule.Repository;
using Hospital.MedicalRecords.Repository;
using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.GraphicalEditor.Repository;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RenovationOfRooms
    {
        public int roomId;

        [Fact]
        public void merge_integration_test()
        {
            //Arrange
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new Hospital.RoomsAndEquipment.Repository.EquipmentRepository(new HospitalContext()));
            Room r1 = service.GetById(3);
            Room r2 = service.GetById(4);
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
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new Hospital.RoomsAndEquipment.Repository.EquipmentRepository(new HospitalContext()));
            Room room = service.GetById(100);
            int numOfRooms = service.getAll().Count;

            //Act
            service.splitRoom(room, "ROOM 4", "something", "ROOM 5", "something");

            //Assert
            service.getAll().Count.ShouldBe(numOfRooms + 1);
        }


        [Fact]
        public void get_suggestion_for_renovation()
        {
            //Arrange
            EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new HospitalContext()));
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new Hospital.RoomsAndEquipment.Repository.EquipmentRepository(new HospitalContext()));
            AppointmentService appService = new AppointmentService(new AppointmentRepository(new HospitalContext()), new DoctorRepository(new HospitalContext()));
            RenovationService renovationService = new RenovationService(new RenovationRepository(new HospitalContext()));


            //Act
            RenovationPeriod period = renovationService.getSuggestions(null, appService, equipmentService, 1, 2, 1639224300000, 1639483500000, 1);


            //Assert
            RenovationPeriod checker = new RenovationPeriod() { StartDate = DateTime.Parse("2021-12-11T08:00:00"), EndDate = DateTime.Parse("2021-12-12T08:00:00") };
            period.EndDate.ShouldBe(checker.EndDate);
            period.StartDate.ShouldBe(checker.StartDate);
        }


        [Fact]
        public void get_suggestion_for_renovation_too_many_days()
        {
            //Arrange
            EquipmentService equipmentService = new EquipmentService(new EquipmentRepository(new HospitalContext()));
            RoomService service = new RoomService(new RoomRepository(new HospitalContext()), new Hospital.RoomsAndEquipment.Repository.EquipmentRepository(new HospitalContext()));
            AppointmentService appService = new AppointmentService(new AppointmentRepository(new HospitalContext()), new DoctorRepository(new HospitalContext()));
            RenovationService renovationService = new RenovationService(new RenovationRepository(new HospitalContext()));


            //Act
            RenovationPeriod period = renovationService.getSuggestions(null, appService, equipmentService, 1, 2, 1639224300000, 1639483500000, 10);


            //Assert
            
            period.ShouldBe(null);
        }



    }
}
