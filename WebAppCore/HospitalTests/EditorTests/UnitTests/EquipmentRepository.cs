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
    public class EquipmentRepositoryTest
    {
        private HospitalContext GetInMemoryRepository()
        {
            DbContextOptions<TestContext> options;
            var builder = new DbContextOptionsBuilder<TestContext>();
            builder.UseInMemoryDatabase("TestDb");
            options = builder.Options;
            HospitalContext hospitalContext = new TestContext(options);
            hospitalContext.Database.EnsureDeleted();
            hospitalContext.Database.EnsureCreated();
            return hospitalContext;
        }

        [Fact]
        public void equipment_repository_test_1()
        {
            HospitalContext context = GetInMemoryRepository();


            
            Equipment eq1 = new Equipment (89, "Bandage", EquipmentType.DYNAMIC, 5, 1 );
            context.AllEquipment.Add(eq1);
            context.SaveChanges();
            
            EquipmentRepository eqRepository = new EquipmentRepository(context);
            List<Equipment> eqpmnt = eqRepository.GetAll();

            eqpmnt.Count.ShouldBe(1);
            
        }

        [Fact]
        public void equipment_repository_test_2()
        {
            HospitalContext context = GetInMemoryRepository();
            Equipment eq1 = new Equipment (1, "Bandage", EquipmentType.DYNAMIC, 5, 1 );
            context.AllEquipment.Add(eq1);
            context.SaveChanges();
            EquipmentRepository eqRepository = new EquipmentRepository(context);
            Equipment eqpmntID = eqRepository.GetByID(1);

            eqpmntID.Id.ShouldBeEquivalentTo(1);

        }
    }
}
