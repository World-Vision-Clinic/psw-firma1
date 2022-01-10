using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Service;
using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Service;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital.ShiftsAndVacations.Model;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class VacationsMapper
    {
        internal static VacationDTO vacationToDTO(Vacation vacation, DoctorService doctorService)
        {
            Doctor d = doctorService.FindById(vacation.DoctorId);
            VacationDTO obj = new VacationDTO(vacation.Id, vacation.Description, vacation.Start, vacation.End, vacation.DoctorId, d.FullName());
           

            return obj;
        }
    }
}
