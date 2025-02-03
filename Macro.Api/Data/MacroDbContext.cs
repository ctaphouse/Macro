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
         public DbSet<Gecko> Geckos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize entity mappings here if needed
        }
    }
}
