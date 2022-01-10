using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Service
{
    public class OnCallShiftService
    {
        IOnCallShiftRepository repository;
        public OnCallShiftService(IOnCallShiftRepository repository)
        {
            this.repository = repository;
        }

        public List<OnCallShift> getAll()
        {
            return repository.GetAll();
        }

        public void updateShift(OnCallShift obj)
        {
            repository.Update(obj);
        }

        public bool deleteShift(int id)
        {
            try
            {
                repository.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void addNewOnCallShift(OnCallShift obj)
        {
            repository.Save(obj);
        }

        public OnCallShift getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
