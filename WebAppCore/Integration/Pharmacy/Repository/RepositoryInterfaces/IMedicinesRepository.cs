using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Repositories.Interfaces
{
   public interface IMedicinesRepository : IGenericRepository<Medicine>
    {
        List<string> GetAllMedicines();
        List<string> GetAllIngredients();
        void EditMedicine(Medicine editedMedicine);
        bool AddOrderedMedicine(Medicine orderedMedicine);
    }
}
