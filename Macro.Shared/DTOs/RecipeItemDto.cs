namespace Macro.Shared.Models
{
    public class RecipeItemDto
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int ItemId { get; set; }
        public required string Measurement { get; set; }
        public decimal GramEquivalent { get; set; }
    }
}
