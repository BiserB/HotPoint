using System.Collections.Generic;

namespace HotPoint.Entities
{
    public class Ingredient: SeededEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public IngredientType Type { get; set; }

        public List<RecipeIngredient> RecipeIngredients { get; set; }
    }
}