using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Repository
{
   public interface IMedicalRecordsRepository
    {
        void UpdatePatientsInformation(MedicalRecord medicalRecord);
        List<int> GetExistingIDs();
        MedicalRecord GetByUsername(string username);
        MedicalRecord GetByPatientID(string patientID);

        List<MedicalRecord> GetAll();
        void Save(MedicalRecord parameter);
        void Delete(string id);
    }
}
