using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macro.Api.Models
{
    public class AdjustedRecipe
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }

        public string Measurement { get; set; }
        
        public int Servings { get; set; }
        
        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }
    }
}
