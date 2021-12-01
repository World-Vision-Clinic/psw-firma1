using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class MedicalRecordsRepository : IMedicalRecordsRepository
    {
        private HospitalContext dbContext = new HospitalContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalRecord> GetAll()
        {
            List<MedicalRecord> records = new List<MedicalRecord>();
            dbContext.MedicalRecords.ToList().ForEach(record => records.Add(record));
            return records;
        }

        public MedicalRecord GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public MedicalRecord GetByPatientID(string patientID)
        {
            throw new NotImplementedException();
        }

        public MedicalRecord GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public List<int> GetExistingIDs()
        {
            throw new NotImplementedException();
        }

        public void Save(MedicalRecord parameter)
        {
            throw new NotImplementedException();
        }


        public void UpdatePatientsInformation(MedicalRecord medicalRecord)
        {
            throw new NotImplementedException();
        }
    }
}
