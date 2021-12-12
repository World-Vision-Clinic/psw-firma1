using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
   public class MedicineTherapy
    {
        public int Id { get; set; }
        public string MedicineID { get; set; }
        public int DurationInDays { get; set; }
        public int TimesPerDay{ get; set; }

        public string Description { get; set; }


        public int TherapyId { get; set; }
        public Medicine Medicine { get; set; }
    }
}
