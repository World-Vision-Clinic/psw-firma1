using Hospital.SharedModel;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.ShiftsAndVacations.Repository
{
    public class OnCallShiftRepository : IOnCallShiftRepository
    {

        private readonly HospitalContext dbContext;

        public OnCallShiftRepository(HospitalContext context)
        {
            this.dbContext = context;
        }
        public void Delete(int id)
        {
            OnCallShift shift = dbContext.OnCallShifts.FirstOrDefault(sh => sh.Id == id);
            dbContext.OnCallShifts.Remove(shift);
            dbContext.SaveChanges();
        }

        public bool Exists(int parameter)
        {
            throw new NotImplementedException();
        }

        public List<OnCallShift> GetAll()
        {
            List<OnCallShift> allShifts = new List<OnCallShift>();
            dbContext.OnCallShifts.ToList().ForEach(sh =>{allShifts.Add(sh);});
            return allShifts;
        }

        public OnCallShift GetByID(int id)
        {
            return dbContext.OnCallShifts.FirstOrDefault(sh=>sh.Id==id);
        }

        public void Save(OnCallShift parameter)
        {
            dbContext.OnCallShifts.Add(parameter);
            dbContext.SaveChanges();
        }

        internal List<OnCallShift> GetByDoctorId(int id)
        {
            List<OnCallShift> allShifts = new List<OnCallShift>();
            dbContext.OnCallShifts.ToList().ForEach(sh => { if(sh.DoctorId == id) allShifts.Add(sh); });
            return allShifts;
        }

        public void Update(OnCallShift parameter)
        {
            dbContext.OnCallShifts.Update(parameter);
            dbContext.SaveChanges();
        }

        internal List<OnCallShift> getDoctorsDuty(int doctorId)
        {
            List<OnCallShift> doctorsOnDuty = new List<OnCallShift>();
            dbContext.OnCallShifts.ToList().ForEach(sh => { if(sh.DoctorId == doctorId) doctorsOnDuty.Add(sh); });
            return doctorsOnDuty;
        }
    }
}
