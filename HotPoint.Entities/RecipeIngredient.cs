
using System.ComponentModel.DataAnnotations.Schema;

namespace HotPoint.Entities
{
    public class RecipeIngredient : SeededEntity
    {
        [Column(Order = 1)]
        public int RecipeId { get; set; }

        public Recipe Recipe { get; set; }

        [Column(Order = 2)]
        public int IngredientId { get; set; }

        public Ingredient Ingredient { get; set; }
    }
}
