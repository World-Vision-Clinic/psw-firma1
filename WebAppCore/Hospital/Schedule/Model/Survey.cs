using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.Schedule.Model
{
    public class Survey : Entity
    {
        public DateTime CreationDate { get; private set; }

        public Survey() { }
        public Survey(int id, DateTime creationDate)
        {
            this.Id = id;
            this.CreationDate = creationDate;
        }
        public Survey(DateTime creationDate)
        {
            this.CreationDate = creationDate;
        }
    }
}
