using Macro.Api.Data;
using Macro.Api.Models;
using Macro.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeItemsController : ControllerBase
    {
        private readonly MacroDbContext _context;

        public RecipeItemsController(MacroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeItemDto>>> GetRecipeItems()
        {
            var recipeItems = await _context.RecipeItems.Select(ri => new RecipeItemDto
            {
                Id = ri.Id,
                RecipeId = ri.RecipeId,
                ItemId = ri.ItemId,
                Measurement = ri.Measurement,
                GramEquivalent = ri.GramEquivalent
            }).ToListAsync();

            return Ok(recipeItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeItemDto>> GetRecipeItem(int id)
        {
            var recipeItem = await _context.RecipeItems.Select(ri => new RecipeItemDto
            {
                Id = ri.Id,
                RecipeId = ri.RecipeId,
                ItemId = ri.ItemId,
                Measurement = ri.Measurement,
                GramEquivalent = ri.GramEquivalent
            }).FirstOrDefaultAsync(ri => ri.Id == id);

            if (recipeItem == null)
            {
                return NotFound();
            }

            return Ok(recipeItem);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeItemDto>> CreateRecipeItem(RecipeItemDto recipeItemDto)
        {
            var recipeItem = new RecipeItem
            {
                RecipeId = recipeItemDto.RecipeId,
                ItemId = recipeItemDto.ItemId,
                Measurement = recipeItemDto.Measurement,
                GramEquivalent = recipeItemDto.GramEquivalent
            };

            _context.RecipeItems.Add(recipeItem);
            await _context.SaveChangesAsync();

            recipeItemDto.Id = recipeItem.Id;

            return CreatedAtAction(nameof(GetRecipeItem), new { id = recipeItemDto.Id }, recipeItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecipeItem(int id, RecipeItemDto recipeItemDto)
        {
            if (id != recipeItemDto.Id)
            {
                return BadRequest();
            }

            var recipeItem = await _context.RecipeItems.FindAsync(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            recipeItem.RecipeId = recipeItemDto.RecipeId;
            recipeItem.ItemId = recipeItemDto.ItemId;
            recipeItem.Measurement = recipeItemDto.Measurement;
            recipeItem.GramEquivalent = recipeItemDto.GramEquivalent;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipeItem(int id)
        {
            var recipeItem = await _context.RecipeItems.FindAsync(id);
            if (recipeItem == null)
            {
                return NotFound();
            }

            _context.RecipeItems.Remove(recipeItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
