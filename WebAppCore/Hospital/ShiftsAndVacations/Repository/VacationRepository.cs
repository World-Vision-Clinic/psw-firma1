using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.ShiftsAndVacations.Repository
{
    public class VacationRepository : IVacationRepository
    {

        private readonly HospitalContext dbContext;

        public VacationRepository(HospitalContext context)
        {
            this.dbContext = context;
        }
        public void Delete(int id)
        {
            Vacation vacation = dbContext.Vacations.FirstOrDefault(v => v.Id == id);
            dbContext.Vacations.Remove(vacation);
            dbContext.SaveChanges();
        }


        public List<Vacation> GetAll()
        {
            List<Vacation> allVacations = new List<Vacation>();
            dbContext.Vacations.ToList().ForEach(v =>{ allVacations.Add(v);});
            return allVacations;
        }

        public Vacation GetByID(int id)
        {
            return dbContext.Vacations.FirstOrDefault(sh=>sh.Id==id);
        }

        public void Save(Vacation parameter)
        {
            dbContext.Vacations.Add(parameter);
            dbContext.SaveChanges();
        }

        public void Update(Vacation parameter)
        {
            dbContext.Vacations.Update(parameter);
            dbContext.SaveChanges();
        }

        public bool Exists(int parameter)
        {
            throw new NotImplementedException();
        }
    }
}
