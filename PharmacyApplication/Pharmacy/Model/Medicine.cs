using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.Model
{
    public class Medicine
    {
        public long MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string Manufacturer { get; set; }
        public string SideEffects { get; set; }
        public string Usage { get; set; }
        public virtual List<SubstituteMedicine> SubstituteMedicines { get; set; }
        public double Weigth { get; set; }
        public double Price{ get; set; }
        public string MainPrecautions { get; set; }
        public string PotentialDangers { get; set; }
        public virtual List<Substance> Substances { get; set; }
        public int Quantity { get; set; }

        public Medicine() { }
        public Medicine(string name, double dosageInMg, int quantity)
        {
            MedicineName = name;
            Weigth = dosageInMg;
            Quantity = quantity;
        }
        public Medicine(long id, string medicineName, string manufacturer, string sideEffects, string usage, List<SubstituteMedicine> substituteMedicines, double weigth, string mainPrecautions, string potentialDangers, List<Substance> substances, int quantity)
        {
            MedicineId = id;
            MedicineName = medicineName;
            Manufacturer = manufacturer;
            SideEffects = sideEffects;
            Usage = usage;
            SubstituteMedicines = substituteMedicines;
            Weigth = weigth;
            MainPrecautions = mainPrecautions;
            PotentialDangers = potentialDangers;
            Substances = substances;
            Quantity = quantity;
        }
    }
}