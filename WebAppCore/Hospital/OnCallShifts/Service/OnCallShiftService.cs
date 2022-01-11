using Hospital.MedicalRecords.Model;
using Hospital.ShiftsAndVacations.Model;
using Hospital.ShiftsAndVacations.Repository;
using Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Service
{
    public class OnCallShiftService
    {
        OnCallShiftRepository repository;
        public OnCallShiftService(OnCallShiftRepository repository)
        {
            this.repository = repository;
        }

        public List<OnCallShift> getAll()
        {
            return repository.GetAll();
        }

        public void updateShift(OnCallShift obj)
        {
            repository.Update(obj);
        }

        public bool deleteShift(int id)
        {
            try
            {
                repository.Delete(id);
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public void addNewOnCallShift(OnCallShift obj)
        {
            repository.Save(obj);
        }

        public int getNumberOfOnCallShifts(Doctor doctor, DateTime startDate, DateTime endDate)
        {
            int numberOfOnCallShifts = 0;
            List<OnCallShift> shifts = repository.GetByDoctorId(doctor.Id);
            foreach (OnCallShift a in shifts)
            {
                if ( startDate <= a.Date && a.Date < endDate)
                    numberOfOnCallShifts++;
            }

            return numberOfOnCallShifts;
        }

        public OnCallShift getById(int id)
        {
            return repository.GetByID(id);
        }

        public void addNewOnCallShift(int id, int doctorId, DateTime date)
        {
            OnCallShift obj = new OnCallShift(id, doctorId, date);
            repository.Save(obj);
        }

        public void updateShift(int id, int doctorId, DateTime date)
        {
            OnCallShift obj = new OnCallShift(id, doctorId, date);
            repository.Update(obj);
        }

        public List<OnCallShift> getDoctorsDuty(int doctorId)
        {
            return repository.getDoctorsDuty(doctorId);
        }
    }
}
