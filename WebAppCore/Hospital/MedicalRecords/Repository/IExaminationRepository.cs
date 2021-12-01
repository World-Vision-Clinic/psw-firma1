using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IExaminationRepository
    {
        List<Examination> GetAllByMedicalRecordId(String medicalRecordId);
        public Therapy GetTherapyById(int id);
    }
}
