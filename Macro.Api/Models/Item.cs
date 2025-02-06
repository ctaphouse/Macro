using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Macro.Api.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Calories { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Protein { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Carbohydrates { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fiber { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Sugars { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Fat { get; set; }
        
        public int ItemTypeId { get; set; }
        
        [ForeignKey("ItemTypeId")]
        public virtual ItemType ItemType { get; set; }
    }
}
