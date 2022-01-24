using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.RoomsAndEquipment.Repository
{
    public class EquipmentTransportationRepository : IEquipmentTransportationRepository
    {
        private readonly HospitalContext dbContext;

        public EquipmentTransportationRepository(HospitalContext context)
        {
            this.dbContext = context;
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(int parameter)
        {
            throw new NotImplementedException();
        }

        public List<EquipmentTransportation> GetAll()
        {
            List<EquipmentTransportation> list = new List<EquipmentTransportation>();
            dbContext.EquipmentTransportationAggregates.ToList().ForEach(a => list.Add(a));
            return list;
        }

        public EquipmentTransportation GetByID(int id)
        {
            EquipmentTransportation eq = dbContext.EquipmentTransportationAggregates.Find(id);
            return eq;
        }

        public void Save(EquipmentTransportation parameter)
        {
            dbContext.EquipmentTransportationAggregates.Add(parameter);
            dbContext.SaveChanges();
        }

        public void Update(EquipmentTransportation parameter)
        {
            dbContext.EquipmentTransportationAggregates.Update(parameter);
            dbContext.SaveChanges();
        }

        public EquipmentTransportation GetAggregate(int roomid, string eqName)
        {
            List<EquipmentTransportation> lista = this.GetAll();
            if (lista != null)
            {
                foreach (EquipmentTransportation t in lista){
                    if (t.RoomFrom.Id == roomid && t.Equipment.Name == eqName)
                    {
                        return t;
                    }                 
                }
            } else return null;
            return null;
        }
        public EquipmentTransportation GetStorageAggregate(string eqName)
        {
            List<EquipmentTransportation> lista = this.GetAll();
            if (lista != null)
            {
                foreach (EquipmentTransportation t in lista)
                {
                    if (t.RoomFrom==null && t.Equipment.Name == eqName)
                    {
                        return t;
                    }
                }
            }
            else return null;
            return null;
        }
    }
}
