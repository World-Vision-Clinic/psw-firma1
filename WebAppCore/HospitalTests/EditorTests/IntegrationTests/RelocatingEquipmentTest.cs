using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel;
using Hospital_API.Controllers;
using Hospital_API.DTO;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.EditorTests.IntegrationTests
{
    public class RelocatingEquipmentTest
    {
        public bool relocate(Equipment eqForTransf, RoomDTO roomFrom, RoomDTO roomTo)
        {
            Equipment eqOld = new Equipment();
            Equipment eq = new Equipment();
            bool flag = false;
            foreach(Equipment equip in roomFrom.equipments)
            {
                if (equip.Name == eqForTransf.Name)
                {
                    if (equip.Amount > eqForTransf.Amount)
                    {
                        flag = true;
                        eqOld = equip;
                        eq = new Equipment(equip.Id, equip.Name, equip.Type, equip.Amount - eqForTransf.Amount, equip.RoomId);
                        roomTo.equipments.Add(eqForTransf);
                        break;
                    }
                }
            }
            if(flag == true)
            {
                roomFrom.equipments.Remove(eqOld);
                roomFrom.equipments.Add(eq);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        [Fact]
        public void relocate_equipment_test()
        {
            Equipment equpmentForTransfer = new Equipment (77, "Bandage", EquipmentType.DYNAMIC, 5, 2 );
            
            var roomController = new RoomsController();
            RoomDTO roomFrom = roomController.GetRoom(2).Value;
            RoomDTO roomTo = roomController.GetRoom(3).Value;

            relocate(equpmentForTransfer, roomFrom, roomTo).ShouldBeTrue();
            //roomController.PutRooms(roomFrom.id, roomFrom);
            //roomController.PutRooms(roomTo.id, roomTo);

        }
    }
}
