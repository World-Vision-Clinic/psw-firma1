using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.ShiftsAndVacations.Repository
{
    public class ShiftRepository : IShiftRepository
    {

        private readonly HospitalContext dbContext;

        public ShiftRepository(HospitalContext context)
        {
            this.dbContext = context;
        }
        public void Delete(int id)
        {
            Shift shift = dbContext.Shifts.FirstOrDefault(sh => sh.Id == id);
            dbContext.Shifts.Remove(shift);
            dbContext.SaveChanges();
        }

        public bool Exists(int parameter)
        {
            throw new NotImplementedException();
        }

        public List<Shift> GetAll()
        {
            List<Shift> allShifts = new List<Shift>();
            dbContext.Shifts.ToList().ForEach(sh =>{allShifts.Add(sh);});
            return allShifts;
        }

        public Shift GetByID(int id)
        {
            return dbContext.Shifts.FirstOrDefault(sh=>sh.Id==id);
        }

        public void Save(Shift parameter)
        {
            dbContext.Shifts.Add(parameter);
            dbContext.SaveChanges();
        }

        public void Update(Shift parameter)
        {
            dbContext.Shifts.Update(parameter);
            dbContext.SaveChanges();
        }
    }
}
