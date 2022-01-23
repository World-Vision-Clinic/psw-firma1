using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital.Schedule.Repository;
using Hospital.SharedModel;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hospital_API.Verification
{
    public class PatientVerification
    {
        private PatientRegisterDTO patient;
        private PatientService patientService;
        private DoctorService doctorService; //Ovde treba injection?
        private AllergenService allergenService;

        public PatientVerification(PatientService patientService, DoctorService doctorService, AllergenService allergenService)
        {
            this.patientService = patientService;
            this.doctorService = doctorService;
            this.allergenService = allergenService;
        }

        public PatientVerification(HospitalContext context)
        {
            PatientRepository patientRepository = new PatientRepository(context);
            AppointmentRepository appointmentRepository = new AppointmentRepository(context);
            patientService = new PatientService(patientRepository, appointmentRepository);
            doctorService = new DoctorService(new DoctorRepository(context, patientRepository));
            allergenService = new AllergenService(new AllergenRepository(context));
        }

        private bool VerifyUsername()
        {
            Regex regex = new Regex("\\A[a-zA-Z0-9]{1,30}\\z");
            if (patient.UserName == null)
                return false;
            if (!regex.IsMatch(patient.UserName))
                return false;
            if (patientService.FindByUserName(patient.UserName) != null)
                return false;
            return true;
        }

        private bool VerifyPassword()
        {
            if (patient.Password == null)
                return false;
            if (patient.Password.Length <= 0 || patient.Password.Length > 100)
                return false;
            return true;
        }

        private bool VerifyFirstName()
        {
            Regex regex = new Regex("\\A[a-zA-ZčćžšđČĆŽŠĐ]{1,20}\\z");
            if (patient.FirstName == null)
                return false;
            if (!regex.IsMatch(patient.FirstName))
                return false;
            return true;
        }

        private bool VerifyLastName()
        {
            Regex regex = new Regex("\\A[a-zA-ZčćžšđČĆŽŠĐ]{1,20}\\z");
            if (patient.LastName == null)
                return false;
            if (!regex.IsMatch(patient.LastName))
                return false;
            return true;
        }

        private bool VerifyEmail()
        {
            int maxLength = 30;
            Regex regex = new Regex("\\A[a-z0-9\\.]+[@][a-z0-9\\.]+\\z", RegexOptions.IgnoreCase);
            if (patient.EMail == null)
                return false;
            if(patient.EMail.Length > maxLength)
                return false;
            if (!regex.IsMatch(patient.EMail))
                return false;
            if (patientService.FindByEmail(patient.EMail) != null)
                return false;
            return true;
        }

        private bool VerifyGender()
        {
            if (patient.Gender == null)
                return false;
            foreach (string gender in Enum.GetNames(typeof(Gender)))
            {
                if(patient.Gender.Equals(gender))
                    return true;
            }
            return false;
        }

        private bool VerifyJmbg()
        {
            Regex regex = new Regex("\\A[0-9]{13}\\z");
            if (patient.Jmbg == null)
                return false;
            if (!regex.IsMatch(patient.Jmbg))
                return false;
            return true;
        }

        private bool VerifyDateOfBirth()
        {
            if (patient.DateOfBirth == null)
                return false;
            if (patient.DateOfBirth > DateTime.Now)
                return false;
            return true;
        }

        private bool VerifyCountry()
        {
            Regex regex = new Regex("\\A[a-zA-ZčćžšđČĆŽŠĐ ]{1,20}\\z");
            if (patient.Country == null)
                return false;
            if (!regex.IsMatch(patient.Country))
                return false;
            return true;
        }

        private bool VerifyAddress()
        {
            Regex regex = new Regex("\\A[a-zA-Z0-9čćžšđČĆŽŠĐ ]{1,30}\\z");
            if (patient.Address == null)
                return false;
            if (!regex.IsMatch(patient.Address))
                return false;
            return true;
        }

        private bool VerifyCity()
        {
            Regex regex = new Regex("\\A[a-zA-ZčćžšđČĆŽŠĐ ]{1,20}\\z");
            if (patient.City == null)
                return false;
            if (!regex.IsMatch(patient.City))
                return false;
            return true;
        }

        private bool VerifyPhone()
        {
            Regex regex = new Regex("\\A[0-9]{1,30}\\z");
            if (patient.Phone == null)
                return false;
            if (!regex.IsMatch(patient.Phone))
                return false;
            return true;
        }

        private bool VerifyAllergens()
        {
            if (patient.Allergens == null)
                return false;
            List<Allergen> allergens = allergenService.GetAllergens();
            foreach(int aid in patient.Allergens)
            {
                bool found = false;
                foreach(Allergen a in allergens)
                {
                    if (a.Id == aid)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    return false;
            }
            return true;
        }

        private bool VerifyPreferedDoctor()
        {
            if (patient.PreferedDoctor < 0)
                return false;
            List<Doctor> availableDoctors = doctorService.GetAvailableDoctors();
            foreach(Doctor d in availableDoctors)
            {
                if (d.Id == patient.PreferedDoctor)
                    return true;
            }
            return true;
        }

        private bool VerifyWeight()
        {
            if (patient.Weight < 0)
                return false;
            return true;
        }

        private bool VerifyHeight()
        {
            if (patient.Height < 0)
                return false;
            return true;
        }

        private bool VerifyBloodType()
        {
            if (patient.BloodType == null)
                return false;
            foreach (string bloodType in Enum.GetNames(typeof(BloodType)))
            {
                if (patient.BloodType.Equals(bloodType))
                    return true;
            }
            return false;
        }

        public bool Verify(PatientRegisterDTO patient)
        {
            this.patient = patient;
            if (patient == null)
                return false;
            if(!VerifyUsername())
                return false;
            if(!VerifyPassword())
                return false;
            if (!VerifyFirstName())
                return false;
            if (!VerifyLastName())
                return false;
            if (!VerifyEmail())
                return false;
            if (!VerifyGender())
                return false;
            if (!VerifyJmbg())
                return false;
            if (!VerifyDateOfBirth())
                return false;
            if (!VerifyCountry())
                return false;
            if (!VerifyAddress())
                return false;
            if (!VerifyCity())
                return false;
            if (!VerifyPhone())
                return false;
            if (!VerifyAllergens())
                return false;
            if (!VerifyWeight())
                return false;
            if (!VerifyHeight())
                return false;
            if (!VerifyBloodType())
                return false;
            if(!VerifyPreferedDoctor())
                return false;
            return true;
        }
    }
}
