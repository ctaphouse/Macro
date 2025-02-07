namespace Macro.Shared.Models
{
    public class ItemDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Fiber { get; set; }
        public decimal Sugars { get; set; }
        public decimal Fat { get; set; }
        public int ItemTypeId { get; set; }
    }
}
