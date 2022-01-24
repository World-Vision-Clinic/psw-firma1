using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class ReportDTO
    {
        public string Content { get; set; }
       

        public ReportDTO() { }
        public ReportDTO(string content)
        {
            Content = content;
        }
      
    }
}
