namespace Macro.Shared.Models
{
    public class AdjustedRecipeDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public string Measurement { get; set; }
        public int Servings { get; set; }
    }
}
