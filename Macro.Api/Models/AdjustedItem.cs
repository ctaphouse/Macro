using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macro.Api.Models
{
    public class AdjustedItem
    {
        [Key]
        public int Id { get; set; }
        public int ItemId { get; set; }
        
        public string Measurement { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GramEquivalent { get; set; }
        
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}
