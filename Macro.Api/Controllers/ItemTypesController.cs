using Macro.Api.Data;
using Macro.Api.Models;
using Macro.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly MacroDbContext _context;

        public ItemTypesController(MacroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemTypeDto>>> GetItemTypes()
        {
            var itemTypes = await _context.ItemTypes.Select(it => new ItemTypeDto
            {
                Id = it.Id,
                Name = it.Name
            }).ToListAsync();

            return Ok(itemTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemTypeDto>> GetItemType(int id)
        {
            var itemType = await _context.ItemTypes.Select(it => new ItemTypeDto
            {
                Id = it.Id,
                Name = it.Name
            }).FirstOrDefaultAsync(it => it.Id == id);

            if (itemType == null)
            {
                return NotFound();
            }

            return Ok(itemType);
        }

        [HttpPost]
        public async Task<ActionResult<ItemTypeDto>> CreateItemType(ItemTypeDto itemTypeDto)
        {
            var itemType = new ItemType
            {
                Name = itemTypeDto.Name
            };

            _context.ItemTypes.Add(itemType);
            await _context.SaveChangesAsync();

            itemTypeDto.Id = itemType.Id;

            return CreatedAtAction(nameof(GetItemType), new { id = itemTypeDto.Id }, itemTypeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemType(int id, ItemTypeDto itemTypeDto)
        {
            if (id != itemTypeDto.Id)
            {
                return BadRequest();
            }

            var itemType = await _context.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            itemType.Name = itemTypeDto.Name;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType(int id)
        {
            var itemType = await _context.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                return NotFound();
            }

            _context.ItemTypes.Remove(itemType);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}