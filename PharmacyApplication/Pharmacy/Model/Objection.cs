using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Objection
    {
        public int Id { get; set; }
        public string HospitalId { get; set; }
        public string Content { get; set; }
        public string IdEncoded { get; set; }

        public Objection() { }

        public Objection(int id, string content, string idEncoded)
        {
            Id = id;
            Content = content;
            IdEncoded = idEncoded;
        }
    }
}
