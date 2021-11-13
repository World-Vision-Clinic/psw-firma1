// File:    Ingredient.cs
// Author:  Ana Gavrilovic
// Created: Tuesday, April 6, 2021 7:08:53 PM
// Purpose: Definition of Class Ingredient

using System;

namespace Integration
{
    public class Ingredient
    {
        public int Id { get; set; }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public override string ToString()
        {   
            return name;
        }

    }
}
