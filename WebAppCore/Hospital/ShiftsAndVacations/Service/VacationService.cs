using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Service
{
    public class VacationService
    {
        IVacationRepository repository;
        public VacationService(IVacationRepository repository)
        {
            this.repository = repository;
        }

        public List<Vacation> getAll()
        {
            return repository.GetAll();
        }

        public void updateVacation(int id,string desc, DateTime start,DateTime end, int doctor)
        {
            Vacation v = new Vacation(id, desc, start, end, doctor);
            repository.Update(v);
        }

        public bool deleteVacation(int id)
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

        public void addNewVacation(int id, string desc, DateTime start, DateTime end, int doctor)
        {
            Vacation v = new Vacation(id, desc, start, end, doctor);
            repository.Save(v);
        }

        public Vacation getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
