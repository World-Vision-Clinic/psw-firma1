using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.MedicalRecords.Model
{
    public class Feedback : Entity
    {
        public string Content { get; private set; }
        public bool IsPublic { get; private set; }
        public bool IsPublishable { get; private set; }
        public bool IsAnonymous { get; private set; }
        public DateTime Date { get; private set; }
        public string UserName { get; set; }

        public Feedback(string content, bool isPublic, bool isPublishable, bool isAnonymous, DateTime date, string userName)
        {
            this.Content = content;
            this.IsPublic = isPublic;
            this.IsPublishable = isPublishable;
            this.IsAnonymous = isAnonymous;
            this.Date = date;
            this.UserName = userName;
        }
    }
}
