namespace Macro.Shared.Models
{
    
    public class RecipeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Servings { get; set; }
    }
}
