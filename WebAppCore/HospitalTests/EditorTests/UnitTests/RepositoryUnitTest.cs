﻿using Hospital.RoomsAndEquipment.Model;
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
    public class RepositoryUnitTest
    {
        [Fact]
        public void RepositoryTest1()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;


            using (var context = new HospitalContext(options))
            {
                Equipment eq1 = new Equipment { id = 89, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 5, RoomId = 1 };
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
        public void RepositoryTest2()
        {
            var options = new DbContextOptionsBuilder<HospitalContext>()
                .UseInMemoryDatabase(databaseName: "HospitalDB")
                .Options;

            using (var context = new HospitalContext(options))
            {
                Equipment eq1 = new Equipment { id = 1, Name = "Bandage", Type = EquipmentType.DYNAMIC, Amount = 5, RoomId = 1 };
                context.AllEquipment.Add(eq1);
                context.SaveChanges();
                EquipmentRepository eqRepository = new EquipmentRepository(context);
                Equipment eqpmntID = eqRepository.GetByID(1);

                eqpmntID.id.ShouldBeEquivalentTo(1);
            }
        }
    }
}
