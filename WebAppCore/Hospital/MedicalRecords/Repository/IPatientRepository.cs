using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IPatientRepository
    {
        public void AddPatient(Patient newPatient);


        public void SaveSync();


        public Patient FindByToken(string token);

        public Patient FindById(int id);


        public void Modify(Patient patient);


    }
}
