using Hospital.MedicalRecords.Model;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Hospital_API.Verification
{
    public class PatientVerification
    {
        private bool VerifyUsername(PatientRegisterDTO patient)
        {
            Regex regex = new Regex("\\A[a-zA-Z0-9]{1,30}\\z");
            if (patient.UserName == null)
                return false;
            if (!regex.IsMatch(patient.UserName))
                return false;
            return true;
        }
        public bool Verify(PatientRegisterDTO patient)
        {
            if(patient == null)
                return false;
            if(!VerifyUsername(patient))
                return false;
            return true;
        }
    }
}
