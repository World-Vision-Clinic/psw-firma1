﻿using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class DoctorService
    {
        private readonly DoctorRepository _repo;

        public DoctorService(DoctorRepository repo)
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

        public void SaveSync()
        {
            _repo.SaveSync();
        }
    }
}
