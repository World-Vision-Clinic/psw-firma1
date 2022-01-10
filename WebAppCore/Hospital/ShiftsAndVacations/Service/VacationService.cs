using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;

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

        public void updateVacation(int id,string desc, DateTime start,DateTime end, int doctorId, string fullName)
        {
            Vacation v = new Vacation(id, desc, start, end, doctorId, fullName);
            repository.Update(v);
        }

        public void updateVacation(Vacation v)
        {
            repository.Update(v);
        }

        public bool deleteVacation(int id)
        {
            Vacation v = repository.GetByID(id);
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

        public void addNewVacation(int id, string desc, DateTime start, DateTime end, int doctorId, string fullName)
        {
            Vacation v = new Vacation(id, desc, start, end, doctorId, fullName);
            repository.Save(v);

        }

        public void addNewVacation(Vacation v)
        {
            repository.Save(v);

        }

        public Vacation getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
