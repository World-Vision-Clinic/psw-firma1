using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class ObserveAppointmentDTO
    {
        public int PatientForeignKey { get; set; }
        public int DoctorForeignKey { get; set; }
        public DateTime Date { get; set; }

        public bool IsCanceled { get; set; }
        public bool IsFinished { get; set; }
        public bool IsUpcoming { get; set; }


        public ObserveAppointmentDTO(int patientId, int doctorId, DateTime date, bool isCancelede)
            {
              
                this.PatientForeignKey = patientId;
                this.DoctorForeignKey = doctorId;
                this.Date = date;
                this.IsCanceled = false;
                this.IsFinished = false;
                this.IsUpcoming = false;



        }



    }
}
