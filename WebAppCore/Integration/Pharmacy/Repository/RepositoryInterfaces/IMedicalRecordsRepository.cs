using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories.Interfaces
{
   public interface IMedicalRecordsRepository : IGenericRepository<MedicalRecord>
    {
        void UpdatePatientsInformation(MedicalRecord medicalRecord);
        List<int> GetExistingIDs();
        MedicalRecord GetByUsername(string username);
        MedicalRecord GetByPatientID(string patientID);

    }
}
