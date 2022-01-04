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
        private readonly IPatientRepository _patientRepository;

        public DoctorRepository()
        {
        }

        public DoctorRepository(HospitalContext context, IPatientRepository patientRepository)
        {
            _context = context;
            _patientRepository = patientRepository;
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

        public List<Doctor> GetDoctorsByType(DoctorType type)
        {
            return _context.Doctors.Where(d => d.Type == type).ToList();
        }

        public List<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public List<Doctor> GetForSpecialty(int specialty)
        {
            return _context.Doctors.Where(d => (int)d.Type == specialty).ToList();
        }

        public List<Doctor> GetAvailableDoctors() //TODO: Refactor
        {
            int maxDifference = 3;

            var query = from p in _context.Patients
                        group p by p.PreferedDoctor into g
                        select new
                        {
                            preferedDoctor = g.Key,
                            count = g.Count()
                        };
            List<Doctor> zeroDoctors = new List<Doctor>();
            foreach (Doctor d in GetAll())
            {
                foreach (var dq in query)
                {
                    if (dq.preferedDoctor == d.Id)
                    {
                        break;
                    }
                    zeroDoctors.Add(d);
                }
            }
            int minCount = -1;
            if (zeroDoctors.Count != 0)
            {
                minCount = 0;
            }
            else
            {
                foreach (var d in query)
                {
                    if (minCount == -1 || d.count < minCount)
                        minCount = d.count;
                }
            }
            if (minCount == -1)
            {
                return GetAll();
            }

            List<int> availableDoctorIds = new List<int>();
            foreach (var d in query)
            {
                if (d.count <= minCount + maxDifference)
                    availableDoctorIds.Add(d.preferedDoctor);
            }
            foreach (Doctor d in zeroDoctors)
            {
                availableDoctorIds.Add(d.Id);
            }
            return GetDoctorsFromList(availableDoctorIds);
        }
    }
}
