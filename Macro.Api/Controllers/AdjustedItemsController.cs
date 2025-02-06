using Macro.Api.Data;
using Macro.Api.Models;
using Macro.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdjustedItemsController : ControllerBase
    {
        private readonly MacroDbContext _context;

        public AdjustedItemsController(MacroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdjustedItemDto>>> GetAdjustedItems()
        {
            var items = await _context.AdjustedItems.Select(i => new AdjustedItemDto
            {
                Id = i.Id,
                ItemId = i.ItemId,
                Measurement = i.Measurement,
                GramEquivalent = i.GramEquivalent
            }).ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdjustedItemDto>> GetAdjustedItem(int id)
        {
            var item = await _context.AdjustedItems.Select(i => new AdjustedItemDto
            {
                Id = i.Id,
                ItemId = i.ItemId,
                Measurement = i.Measurement,
                GramEquivalent = i.GramEquivalent
            }).FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<AdjustedItemDto>> CreateAdjustedItem(AdjustedItemDto adjustedItemDto)
        {
            var adjustedItem = new AdjustedItem
            {
                ItemId = adjustedItemDto.ItemId,
                Measurement = adjustedItemDto.Measurement,
                GramEquivalent = adjustedItemDto.GramEquivalent
            };

            _context.AdjustedItems.Add(adjustedItem);
            await _context.SaveChangesAsync();

            adjustedItemDto.Id = adjustedItem.Id;

            return CreatedAtAction(nameof(GetAdjustedItem), new { id = adjustedItemDto.Id }, adjustedItemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdjustedItem(int id, AdjustedItemDto adjustedItemDto)
        {
            if (id != adjustedItemDto.Id)
            {
                return BadRequest();
            }

            var adjustedItem = await _context.AdjustedItems.FindAsync(id);
            if (adjustedItem == null)
            {
                return NotFound();
            }

            adjustedItem.ItemId = adjustedItemDto.ItemId;
            adjustedItem.Measurement = adjustedItemDto.Measurement;
            adjustedItem.GramEquivalent = adjustedItemDto.GramEquivalent;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdjustedItem(int id)
        {
            var adjustedItem = await _context.AdjustedItems.FindAsync(id);
            if (adjustedItem == null)
            {
                return NotFound();
            }

            _context.AdjustedItems.Remove(adjustedItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
