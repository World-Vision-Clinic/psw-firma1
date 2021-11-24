using Hospital.MedicalRecords.Model;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class MedicalRecordMapper
    {
        public static MedicalRecordDTO PatientToMedicalRecordDTO(Patient patientToConvert)
        {
            MedicalRecordDTO newMedicalRecord = new MedicalRecordDTO(patientToConvert);
            return newMedicalRecord;
        }
        public MedicalRecordMapper() { }
    }
}
