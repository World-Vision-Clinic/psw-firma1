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
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.Id == id);
            dbContext.AllEquipment.Remove(eq);
            dbContext.SaveChanges();
        }

        public List<Equipment> GetRoomEquipemnts(int roomId)
        {
            List<Equipment> allEq = new List<Equipment>();
            try
            {
                dbContext.AllEquipment.ToList().ForEach(eq =>
                {
                    if (eq.RoomId == roomId) 
                        allEq.Add(eq);
                });
            }
            catch(Exception e)
            {

            }
            
            
            return allEq;
        }

        internal List<Equipment> GetAllInTransport(List<int> roomIds)
        {
            List<Equipment> allEq = new List<Equipment>();
            try
            {
                dbContext.AllEquipment.ToList().ForEach(eq =>
                {
                    if (roomIds.Contains(eq.RoomId) && (bool)eq?.InTransport) allEq.Add(eq);
                });
            }
            catch
            {

            }


            return allEq;
        }

        internal IEnumerable<Equipment> GetByNameInBuilding(List<int> roomIds, string equipmentName)
        {
            List<Equipment> allEq = new List<Equipment>();

            dbContext.AllEquipment.ToList().ForEach(eq =>
            {
                if (roomIds.Contains(eq.RoomId) && eq.Name == equipmentName && !eq.InTransport)
                {
                    allEq.Add(eq);
                }
            });

            return allEq;
        }

        internal List<Equipment> getUniqueInBuilding(List<int> roomIds)
        {
            List<Equipment> allEq = new List<Equipment>();
            List<string> names = new List<string>();
           
            dbContext.AllEquipment.ToList().ForEach(eq =>
            {
                if (roomIds.Contains(eq.RoomId) && !names.Contains(eq.Name)) {
                    allEq.Add(eq);
                    names.Add(eq.Name);
                }
            });
           
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
            Equipment eq = dbContext.AllEquipment.FirstOrDefault(eq => eq.Id == id);
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

        internal int generateId()
        {
            int id = dbContext.AllEquipment.Max(u => u.Id);
            return id + 1;
        }
    }
}
