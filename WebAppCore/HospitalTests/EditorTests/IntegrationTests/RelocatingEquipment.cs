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
    public class RelocatingEquipment
    {
        public bool relocate(Equipment eqForTransf, RoomDTO roomFrom, RoomDTO roomTo)
        {
            foreach(Equipment equip in roomFrom.equipments)
            {
                if (equip.Name == eqForTransf.Name)
                {
                    if (equip.Amount > eqForTransf.Amount)
                    {
                        equip.Amount = equip.Amount - eqForTransf.Amount;
                        roomTo.equipments.Add(eqForTransf);
                        return true;
                    }
                }
            }
            return false;
        }

        [Fact]
        public void relocate_equipment_test()
        {
            Equipment equpmentForTransfer = new Equipment { Id = 77, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 5, RoomId = 2 };
            
            var roomController = new RoomsController();
            RoomDTO roomFrom = roomController.GetRoom(2).Value;
            RoomDTO roomTo = roomController.GetRoom(3).Value;

            relocate(equpmentForTransfer, roomFrom, roomTo).ShouldBeTrue();
            //roomController.PutRooms(roomFrom.id, roomFrom);
            //roomController.PutRooms(roomTo.id, roomTo);

        }
    }
}
