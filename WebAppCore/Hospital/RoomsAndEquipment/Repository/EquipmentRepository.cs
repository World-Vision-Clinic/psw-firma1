using Hospital.RepositoryInterfaces;
using Hospital.RoomsAndEquipment.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.RoomsAndEquipment.Repository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly HospitalContext dbContext;

        public EquipmentRepository(HospitalContext context)
        {
            dbContext = context;
        }

        public void Delete(int id)
        {
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.id == id);
            dbContext.AllEquipment.Remove(eq);
            dbContext.SaveChanges();
        }

        internal List<Equipment> GetRoomEquipemnts(int roomId)
        {
            List<Equipment> allEq = new List<Equipment>();
            try
            {
                dbContext.AllEquipment.ToList().ForEach(eq =>
                {
                    if (eq.RoomId == roomId) allEq.Add(eq);
                });
            }
            catch
            {

            }
            
            
            return allEq;
        }

        public List<Equipment> GetAll()
        {
            List<Equipment> allEq = new List<Equipment>();
            dbContext.AllEquipment.ToList().ForEach(eq => allEq.Add(eq));
            return allEq;
        }

        public Equipment GetByID(int id)
        {
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.id == id);
            return eq;
        }

        public void Save(Equipment eq)
        {
            dbContext.AllEquipment.Add(eq);
            dbContext.SaveChanges();
        }

        public void Update(Equipment eq)
        {
            dbContext.AllEquipment.Update(eq);
            dbContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return false;
        }
    }
}
