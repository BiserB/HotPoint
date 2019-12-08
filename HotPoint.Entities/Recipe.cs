using System;
using System.Collections.Generic;
using System.Text;

namespace HotPoint.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? ProductId { get; set; }

        public Product Product { get; set; }

        public string Directions { get; set; }

        public string NutritionFacts { get; set; }

        public string Notes { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}
