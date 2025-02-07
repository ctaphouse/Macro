using System.ComponentModel.DataAnnotations;

namespace Macro.Api.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int Servings { get; set; }
    }
}
