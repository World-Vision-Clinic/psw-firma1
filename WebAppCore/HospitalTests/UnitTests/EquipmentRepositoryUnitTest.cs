using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HospitalTests.EditorTests
{
    public class EquipmentRepositoryUnitTest
    {
        [Fact]
        public void equipment_repository_test_1()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;


            using (var context = new HospitalContext(options))
            {
                Equipment eq1 = new Equipment { Id = 89, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 5, RoomId = 1 };
                context.AllEquipment.Add(eq1);
                context.SaveChanges();
            }
            using (var context = new HospitalContext(options))
            {
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                List<Equipment> eqpmnt = eqRepository.GetAll();

                Assert.Equal(2, eqpmnt.Count);
            }
        }

        [Fact]
        public void equipment_repository_test_2()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;

            using (var context = new HospitalContext(options))
            {
                Equipment eq1 = new Equipment { Id = 1, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 5, RoomId = 1 };
                context.AllEquipment.Add(eq1);
                context.SaveChanges();
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                Equipment eqpmntID = eqRepository.GetByID(1);

                eqpmntID.Id.ShouldBeEquivalentTo(1);
            }
        }
    }
}
