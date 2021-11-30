using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public class Allergen
    {
        public int Id { get; set; }
        public string Name { get; set; } //TODO: promeniti name u veliko
        [NotMapped]
        private List<string> medicineNames = new List<string>();
        [NotMapped]
        private List<string> ingredientNames = new List<string>();
        [NotMapped]
        public List<string> MedicineNames
        {
            get { return medicineNames; }
            set { medicineNames = value; }
        }
        [NotMapped]
        public List<string> IngredientNames
        {
            get { return ingredientNames; }
            set { ingredientNames = value; }
        }

    }
}
