using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository.RepositoryInterfaces;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public EquipmentTransportation GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(EquipmentTransportation parameter)
        {
            throw new NotImplementedException();
        }

        public void Update(EquipmentTransportation parameter)
        {
            throw new NotImplementedException();
        }
    }
}
