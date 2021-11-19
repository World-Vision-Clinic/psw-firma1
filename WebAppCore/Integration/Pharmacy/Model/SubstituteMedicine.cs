﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Pharmacy.Model
{
    class SubstituteMedicine
    {
        public long MedicineId { get; set; }
        public virtual Medicine Medicine { get; set; }
        public long SubstituteId { get; set; }
        public virtual Medicine Substitute { get; set; }

        public SubstituteMedicine() { }
    }
}
