using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Service
{
    public class EquipmentTransportationService
    {
        private EquipmentTransportationRepository repository;

        public EquipmentTransportationService(EquipmentTransportationRepository reposit)
        {
            this.repository = reposit;
        }

        public void makeTransportFromStorage(int equipmentId, Equipment e, Room r, Room r1, DateTime d1, DateTime d2)
        {
            EquipmentTransportation eqTrans = repository.GetStorageAggregate(e.Name);
            if (eqTrans == null)
            {
                EquipmentTransportation aggregate = new EquipmentTransportation(e, null, r1, d1, d2);
                aggregate.fromStorageTransport();
                repository.Save(aggregate);
            } else
            {
                eqTrans.fromStorageTransport();
                repository.Save(eqTrans);
            }

        }
    }
}
