using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalContext _context;

        public PatientRepository()
        {
        }

        public PatientRepository(HospitalContext context)
        {
            _context = context;
        }

        public void AddPatient(Patient newPatient)
        {
            _context.Patients.Add(newPatient);
            SaveSync();
        }

        public void SaveSync()
        {
            _context.SaveChanges();
        }

        public Patient FindByToken(string token)
        {
            return _context.Patients.FirstOrDefault(p => p.Token == token);
        }

        public Patient FindByUserName(string username)
        {
            try
            { 
                return _context.Patients.FirstOrDefault(p => p.UserName == username);
            }
            catch
            {
                return null;
            }
        }

        public Patient FindById(int id)
        {
            return _context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void Modify(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            SaveSync();
        }
    }
}
