using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class ExaminationRepository : IExaminationRepository
    {
        private HospitalContext dbContext = new HospitalContext();
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Examination> GetAllByMedicalRecordId(String medicalRecordId)
        {
            List<Examination> examinations = new List<Examination>();
            var list = dbContext.Examinations.ToList().Where(examination => examination.MedicalRecordId == medicalRecordId);
            foreach (Examination e in list)
                examinations.Add(e);
            return examinations;
        }

        public Examination GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Examination parameter)
        {
            throw new NotImplementedException();
        }

        public Therapy GetTherapyById(int id)
        {
            Therapy therapy = new Therapy();
            therapy = dbContext.Therapies.Find(id);
            return therapy;
        }
    }
}
