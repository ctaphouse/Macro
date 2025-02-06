using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macro.Api.Models
{
    public class RecipeItem
    {
        [Key]
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int ItemId { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Measurement { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal GramEquivalent { get; set; }
        
        [ForeignKey("RecipeId")]
        public virtual Recipe Recipe { get; set; }
        
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
    }
}
