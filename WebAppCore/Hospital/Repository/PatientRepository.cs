﻿using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Repository
{
    public class PatientRepository
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

        public void Modify(Patient patient)
        {
            _context.Entry(patient).State = EntityState.Modified;
            SaveSync();
        }
    }
}
