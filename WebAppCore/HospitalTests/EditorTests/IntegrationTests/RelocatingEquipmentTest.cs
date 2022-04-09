using Hospital.RoomsAndEquipment.Model;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Shouldly;
using System.Net.Http;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RelocatingEquipmentTest
    {
        public bool relocate(Equipment eqForTransf, RoomDTO roomFrom, RoomDTO roomTo)
        {
            foreach (Equipment equip in roomFrom.equipments)
            {
                if (!(equip.Name.Equals(eqForTransf.Name) && equip.Amount > eqForTransf.Amount))
                    continue;

                Equipment eqOld = equip;
                Equipment eq = new Equipment(equip.Id, equip.Name, equip.Type, equip.Amount - eqForTransf.Amount, equip.RoomId);
                roomTo.equipments.Add(eqForTransf);
                roomFrom.equipments.Remove(eqOld);
                roomFrom.equipments.Add(eq);
                return true;
            }

            return false;
        }

        [Fact]
        public void relocate_equipment_test()
        {
            Equipment equpmentForTransfer = new Equipment(77, "Bandage", EquipmentType.DYNAMIC, 5, 2);
            var roomController = new RoomsController();
            RoomDTO roomFrom = roomController.GetRoom(2).Value;
            RoomDTO roomTo = roomController.GetRoom(3).Value;
            bool expected = relocate(equpmentForTransfer, roomFrom, roomTo);

            HttpResponseMessage result = roomController.Relocate(equpmentForTransfer, roomFrom.id, roomTo.id);

            (result.StatusCode.Equals(200)).ShouldBe(expected);

        }
    }
}
