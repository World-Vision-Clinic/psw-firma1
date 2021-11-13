using Integration;
using Integration.Repositories.Interfaces;
using Integration.SharedModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository
{
    public class MedicalRecordsRepository : IMedicalRecordsRepository
    {
        private IntegrationDbContext dbContext = new IntegrationDbContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<MedicalRecord> GetAll()
        {
            throw new NotImplementedException();
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
