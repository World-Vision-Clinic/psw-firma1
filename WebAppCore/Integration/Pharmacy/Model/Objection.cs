using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class Objection
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string PharmacyId { get; set; }

        public Objection(string id, string content, string pharmacyId)
        {
            Id = id;
            Content = content;
            PharmacyId = pharmacyId;
        }

        public Objection() { }
    }
}

