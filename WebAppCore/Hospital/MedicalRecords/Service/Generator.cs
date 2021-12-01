using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class Generator
    {
        public static string GenerateMedicineId()
        {
            HospitalContext context = new HospitalContext();
            Medicine foundedMedicine = null;
            string medicineId = "";
            do
            {
                var key = new byte[32];
                using (var generator = RandomNumberGenerator.Create())
                    generator.GetBytes(key);
                medicineId = Convert.ToBase64String(key);
                foundedMedicine = context.Medicines.SingleOrDefault(medicine => medicine.ID == medicineId);

            } while (foundedMedicine != null || medicineId.Contains("+"));

            return medicineId;
        }
    }
}
