using Microsoft.EntityFrameworkCore;
using Macro.Api.Models;

namespace Macro.Api.Data
{
    public class MacroDbContext : DbContext
    {
        public MacroDbContext(DbContextOptions<MacroDbContext> options)
            : base(options)
        {
        }

        // Add your DbSet properties here
         public DbSet<AdjustedItem> AdjustedItems { get; set; }
         public DbSet<AdjustedRecipe> AdjustedRecipes { get; set; }
         public DbSet<Item> Items { get; set; }
         public DbSet<ItemType> ItemTypes{ get; set; }
         public DbSet<Recipe> Recipes{ get; set; }
         public DbSet<RecipeItem> RecipeItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize entity mappings here if needed
        }
    }
}
