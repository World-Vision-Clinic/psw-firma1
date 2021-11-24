
using System;

namespace Integration
{
    public class Examination
    {
        public int Id { get; set; }
        public String Diagnosis { get; set; }
        public String anamnesis { get; set; }
        public DateTime dateOfExamination { get; set; }
        public Therapy therapy { get; set; }
        public int TherapyId { get; set; }
        public Appointment appointment { get; set; }
        public Boolean patientVisible { get; set; }
        public String MedicalRecordId { get; set; }

        public Examination()
        {
            therapy = new Therapy();
            appointment = new Appointment();
            patientVisible = true;
        }
    }
}