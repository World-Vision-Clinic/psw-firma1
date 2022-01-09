using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repo;
        private readonly IShiftRepository shiftRepository;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }
        public DoctorService(IDoctorRepository repo, IShiftRepository shiftRepo)
        {
            _repo = repo;
            shiftRepository = shiftRepo;
        }

        public void AddDoctor(Doctor doctor)
        {
            _repo.AddDoctor(doctor);
        }

        public Doctor FindById(int id)
        {
            return _repo.FindById(id);
        }

        public void Modify(Doctor doctor)
        {
            _repo.Modify(doctor);
        }
        public List<Doctor> GetDoctorsFromList(List<int> doctorIds)
        {
            return _repo.GetDoctorsFromList(doctorIds);
        }

        public List<Doctor> GetAvailableDoctors()
        {
            return _repo.GetAvailableDoctors();
        }

        public List<Doctor> GetDoctorsByType(DoctorType type)
        {
            return _repo.GetDoctorsByType(type);
        }

        public List<Doctor> GetAll()
        {
            return _repo.GetAll();
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

        public void AddShiftToDoctor(int doctorID, int shiftID)
        {
            Doctor d = this.FindById(doctorID);
            d.changeShift(shiftID);
            _repo.Modify(d);
            _repo.SaveSync();
        }

        public void updateDoctor(Doctor doctor)
        {
            _repo.Update(doctor);
        }

        public void addNewDoctor(int id, string firstName, string lastName,int shiftId, int roomId, DoctorType type, bool onVacation)
        {
            Doctor d = new Doctor(id, firstName, lastName, shiftId, roomId, type, false);
            _repo.Save(d);
        }

    }
}
