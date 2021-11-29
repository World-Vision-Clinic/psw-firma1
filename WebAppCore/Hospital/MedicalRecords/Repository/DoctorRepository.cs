using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalContext _context;

        public DoctorRepository()
        {
        }

        public DoctorRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            SaveSync();
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public Doctor FindById(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id);
        }

        public void Modify(Doctor doctor)
        {
            _context.Entry(doctor).State = EntityState.Modified;
            SaveSync();
        }

        public List<Doctor> GetDoctorsFromList(List<int> doctorIds)
        {
            List<Doctor> doctors = new List<Doctor>();
            foreach (int id in doctorIds)
            {
                Doctor doctor = FindById(id);
                doctors.Add(doctor);
            }
            return doctors;
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public List<Doctor> GetAvailableDoctors() //TODO: Refactor
        {
            int maxDifference = 3;
            return GetAll();
            /*
            var query = from p in _context.Patients
                        group p by p.PreferedDoctor into g
                        select new
                        {
                            preferedDoctor = g.Key,
                            count = g.Count()
                        };
            int minCount = -1;
            foreach(var d in query)
            {
                if(minCount == -1 || d.count < minCount)
                    minCount = d.count;
            }
            if (minCount == -1)
            {
                return GetAll();
            }
            else
            {
                foreach(Doctor d in GetAll())
                {
                    bool found = false;
                    foreach(Patient p in _patientRepository.GetAll())
                    {
                        if (p.PreferedDoctor == d.Id)
                        {
                            found = true;
                            break;
                        }
                        if (!found)
                        {
                            minCount = 0;
                            break;
                        }
                    }
                }
            }
            List<int> availableDoctorIds = new List<int>();
            foreach (var d in query)
            {
                if(d.count <= minCount + maxDifference)
                    availableDoctorIds.Add(d.preferedDoctor);
            }
            

            return GetDoctorsFromList(availableDoctorIds);
            */
        }
    }
}
