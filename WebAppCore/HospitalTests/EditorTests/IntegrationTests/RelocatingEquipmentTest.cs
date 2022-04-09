using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.RoomsAndEquipment.Service;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Shouldly;
using System.Net;
using System.Net.Http;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RelocatingEquipmentTest
    {

        private void pripareDatabase()
        {
            EquipmentRepository equipmentRepository = new EquipmentRepository(new HospitalContext());
            if (equipmentRepository.GetByID(77) != null)
                equipmentRepository.Delete(77);

            Equipment equipment = equipmentRepository.GetRoomEquipemnts(2).Find(e => e.Name.Equals("Bandage"));
            if (equipment.Amount < 6)
            {
                Equipment newEqupment = new Equipment(equipment.Id, equipment.Name, equipment.Type, equipment.Amount + 5, equipment.RoomId);
                equipmentRepository.Delete(equipment.Id);
                equipmentRepository.Save(newEqupment);
                
            }
        }

        
        [Theory]
        [InlineData(77, "Bandage", HttpStatusCode.OK)]
        [InlineData(78, "Bed", HttpStatusCode.BadRequest)]
        public void relocate_equipment_test(int equpId, string equipName, HttpStatusCode expected)
        {
            pripareDatabase();
            Equipment equpmentForTransfer = new Equipment(equpId, equipName, EquipmentType.DYNAMIC, 5, 2);
            var roomController = new RoomsController();
            RoomDTO roomFrom = roomController.GetRoom(2).Value;
            RoomDTO roomTo = roomController.GetRoom(3).Value;

            HttpResponseMessage result = roomController.Relocate(equpmentForTransfer, roomFrom.id, roomTo.id);

            result.StatusCode.ShouldBe(expected);

        }
    }
}
