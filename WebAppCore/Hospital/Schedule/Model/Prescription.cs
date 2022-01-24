using Hospital.MedicalRecords.Model;
using Hospital.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Schedule.Model
{
    [Keyless]
    [NotMapped]
    public class Prescription : ValueObject
    {
        public string Description { get; set; }

        Prescription(string description, List<int> medicines)
        {
            Description = description;
        }

        Prescription() { }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}
