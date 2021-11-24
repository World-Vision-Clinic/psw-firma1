using Integration.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Repository.RepositoryInterfaces
{
    public interface IExaminationRepository : IGenericRepository<Examination>
    {
        List<Examination> GetAllByMedicalRecordId(String medicalRecordId);
        public Therapy GetTherapyById(int id);
    }
}
