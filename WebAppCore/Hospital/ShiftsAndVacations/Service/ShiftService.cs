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

        public void updateShift(int id,string name, int start,int end)
        {
            Shift s = new Shift(id, name, start, end);
            repository.Update(s);
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

        public void addNewShift(int id, string name, int start, int end)
        {
            Shift s = new Shift(id, name, start, end);
            repository.Save(s);
        }

        public Shift getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
