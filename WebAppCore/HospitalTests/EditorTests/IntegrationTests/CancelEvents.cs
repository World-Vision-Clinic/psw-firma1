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
    }
}
