using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IDoctorRepository
    {
        public void AddDoctor(Doctor doctor);

        public void SaveSync();

        public Doctor FindById(int id);

        public void Modify(Doctor doctor);
        public List<Doctor> GetDoctorsFromList(List<int> doctorIds);
        public List<Doctor> GetAvailableDoctors();
        public List<Doctor> GetDoctorsByType(DoctorType type);
        public List<Doctor> GetAll();
    }
}
