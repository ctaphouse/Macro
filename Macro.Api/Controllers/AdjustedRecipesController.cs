using Macro.Api.Data;
using Macro.Api.Models;
using Macro.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macro.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdjustedRecipesController : ControllerBase
    {
        private readonly MacroDbContext _context;

        public AdjustedRecipesController(MacroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdjustedRecipeDto>>> GetAdjustedRecipes()
        {
            var recipes = await _context.AdjustedRecipes.Select(r => new AdjustedRecipeDto
            {
                Id = r.Id,
                RecipeId = r.RecipeId,
                Measurement = r.Measurement,
                Servings = r.Servings
            }).ToListAsync();

            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdjustedRecipeDto>> GetAdjustedRecipe(int id)
        {
            var recipe = await _context.AdjustedRecipes.Select(r => new AdjustedRecipeDto
            {
                Id = r.Id,
                RecipeId = r.RecipeId,
                Measurement = r.Measurement,
                Servings = r.Servings
            }).FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }

        [HttpPost]
        public async Task<ActionResult<AdjustedRecipeDto>> CreateAdjustedRecipe(AdjustedRecipeDto adjustedRecipeDto)
        {
            var adjustedRecipe = new AdjustedRecipe
            {
                RecipeId = adjustedRecipeDto.RecipeId,
                Measurement = adjustedRecipeDto.Measurement,
                Servings = adjustedRecipeDto.Servings
            };

            _context.AdjustedRecipes.Add(adjustedRecipe);
            await _context.SaveChangesAsync();

            adjustedRecipeDto.Id = adjustedRecipe.Id;

            return CreatedAtAction(nameof(GetAdjustedRecipe), new { id = adjustedRecipeDto.Id }, adjustedRecipeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdjustedRecipe(int id, AdjustedRecipeDto adjustedRecipeDto)
        {
            if (id != adjustedRecipeDto.Id)
            {
                return BadRequest();
            }

            var adjustedRecipe = await _context.AdjustedRecipes.FindAsync(id);
            if (adjustedRecipe == null)
            {
                return NotFound();
            }

            adjustedRecipe.RecipeId = adjustedRecipeDto.RecipeId;
            adjustedRecipe.Measurement = adjustedRecipeDto.Measurement;
            adjustedRecipe.Servings = adjustedRecipeDto.Servings;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdjustedRecipe(int id)
        {
            var adjustedRecipe = await _context.AdjustedRecipes.FindAsync(id);
            if (adjustedRecipe == null)
            {
                return NotFound();
            }

            _context.AdjustedRecipes.Remove(adjustedRecipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
