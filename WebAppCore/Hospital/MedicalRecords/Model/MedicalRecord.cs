using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class MedicalRecord
    {
        private string healthCardNumber;
        private string parentName;
        private bool isInsured;
        private string medicalRecordID;
        private Patient patient = new Patient();
        private List<Examination> examination = new List<Examination>();
        private Allergen allergen = new Allergen();

        public MedicalRecord() { }

        public Allergen Allergen
        {
            get => allergen;
            set
            {
                allergen = value;
            }
        }
          
        public Patient Patient
        {
            get => patient;
            set
            {
                patient = value;
            }
        }

        public string HealthCardNumber
        {
            get => healthCardNumber;
            set
            {
                healthCardNumber = value;
            }
        }

        public string ParentName
        {
            get => parentName;
            set
            {
                parentName = value;
            }
        }

        public bool IsInsured
        {
            get => isInsured;
            set
            {
                isInsured = value;
            }
        }

        public string MedicalRecordID
        {
            get => medicalRecordID;
            set
            {
                medicalRecordID = value;
            }
        }

        public List<Examination> Examination
        {
            get
            {
                if (examination == null)
                    examination = new List<Examination>();
                return examination;
            }
            set
            {
                RemoveAllExamination();
                if (value != null)
                {
                    foreach (Examination oExamination in value)
                        AddExamination(oExamination);
                }
            }
        }

        public void AddExamination(Examination newExamination)
        {
            if (newExamination == null)
                return;
            if (this.examination == null)
                this.examination = new List<Examination>();
            if (!this.examination.Contains(newExamination))
                this.examination.Add(newExamination);
        }

        public void RemoveExamination(Examination oldExamination)
        {
            if (oldExamination == null)
                return;
            if (this.examination != null)
                if (this.examination.Contains(oldExamination))
                    this.examination.Remove(oldExamination);
        }

        public void RemoveAllExamination()
        {
            if (examination != null)
                examination.Clear();
        }

        override
        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append(this.Patient.FullName.FirstName).Append(" ").Append(this.Patient.FullName.LastName).Append(" | ").Append(this.Patient.Id);

            return stringBuilder.ToString();
        }

        public bool HasSameIDAs(MedicalRecord medicalRecord)
        {
            return this.medicalRecordID.Equals(medicalRecord.medicalRecordID);
        }

       
    }
}