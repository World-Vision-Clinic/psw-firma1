using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Service
{
    public class ShiftService
    {
        IShiftRepository repository;
        public ShiftService(IShiftRepository repository)
        {
            this.repository = repository;
        }

        public List<Shift> getAll()
        {
            return repository.GetAll();
        }

        public void updateShift(Shift shift)
        {
            repository.Update(shift);
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

        public void addNewShift(Shift shift)
        {
            repository.Save(shift);
        }

        public Shift getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
