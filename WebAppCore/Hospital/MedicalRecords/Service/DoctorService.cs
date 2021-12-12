using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repo;

        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
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
    }
}
