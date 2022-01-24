using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.GraphicalEditor.Service;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class CancelEvents
    {
        [Fact]
        public void cancel_equipment_24hours_error()
        {
            //Arrange
            EquipmentService service = new EquipmentService(new EquipmentRepository(new HospitalContext()));
            
            
            //Act
            bool success = service.canceledTransport(5);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public void cancel_equipment_success()
        {
            //Arrange
            EquipmentService service = new EquipmentService(new EquipmentRepository(new HospitalContext()));


            //Act
            bool success = service.canceledTransport(4);

            //Assert
            Assert.True(success);
        }

        [Fact]
        public void cancel_renovationt_24hours_error()
        {
            RenovationService service = new RenovationService(new RenovationRepository(new HospitalContext()));
            RoomService roomService = new RoomService(new RoomRepository(new HospitalContext()), new EquipmentRepository(new HospitalContext()));

            //Act
            Renovation r = new Renovation { Room1Id = 2, Room2Id = 3, isMerge = true, NewRoomName1 = "Room 12", StartDate = new DateTime(2022, 01, 12), EndDate = new DateTime(2022, 01, 24), NewRoomPurpose1 = "" };

            int newId = service.DoMerge(roomService, r);
            bool success = service.cancelRenovation(3);

            //Assert
            Assert.False(success);
        }

        [Fact]
        public void cancel_renovation_success()
        {
            RenovationService service = new RenovationService(new RenovationRepository(new HospitalContext()));
            RoomService roomService = new RoomService(new RoomRepository(new HospitalContext()), new EquipmentRepository(new HospitalContext()));

            //Act
            Renovation r = new Renovation { Room1Id = 11, Room2Id = 12, isMerge = true, NewRoomName1 = "Room 14", StartDate = new DateTime(2022, 01, 21), EndDate = new DateTime(2022, 01, 24), NewRoomPurpose1 = "" };

            int newId = service.DoMerge(roomService, r);
            bool success = service.cancelRenovation(2);

            //Assert
            Assert.True(success);
        }
    }
}
