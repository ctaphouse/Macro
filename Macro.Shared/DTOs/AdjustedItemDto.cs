namespace Macro.Shared.Models
{
    public class AdjustedItemDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Measurement { get; set; }
        public decimal GramEquivalent { get; set; }
    }
}
