using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Service
{
    public class VacationService
    {
        IVacationRepository repository;
        IDoctorRepository doctorRepository;

        public VacationService(IVacationRepository repository, IDoctorRepository drRepo)
        {
            this.repository = repository;
            this.doctorRepository = drRepo;
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

        public bool deleteVacation(int id)
        {
            Vacation v = repository.GetByID(id);
            try
            {
                repository.Delete(id);
                //now doctor is not on vacation
                Doctor d = doctorRepository.FindById(v.DoctorId);
                Doctor newDoctor = new Doctor(d.Id, d.FirstName, d.LastName, d.ShiftId, d.RoomId, d.Type, false);
                doctorRepository.Delete(d.Id);
                doctorRepository.AddDoctor(newDoctor);
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
            //now doctor is on vacation
            Doctor d = doctorRepository.GetByID(doctorId);
            Doctor newDoctor = new Doctor(d.Id, d.FirstName, d.LastName, d.ShiftId, d.RoomId, d.Type, true);
            doctorRepository.Delete(d.Id);
            doctorRepository.AddDoctor(newDoctor);
            repository.Save(v);

        }

        public Vacation getById(int id)
        {
            return repository.GetByID(id);
        }

    }
}
