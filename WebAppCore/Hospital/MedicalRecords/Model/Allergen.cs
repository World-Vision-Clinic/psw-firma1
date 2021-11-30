using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class Allergen
    {
        public int Id { get; set; }
        public string Name { get; set; } //TODO: promeniti name u veliko
        private List<string> medicineNames = new List<string>();
        private List<string> ingredientNames = new List<string>();
        public List<string> MedicineNames
        {
            get { return medicineNames; }
            set { medicineNames = value; }
        }
        public List<string> IngredientNames
        {
            get { return ingredientNames; }
            set { ingredientNames = value; }
        }

    }
}
