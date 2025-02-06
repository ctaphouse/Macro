using Macro.Api.Data;
using Macro.Api.Models;
using Macro.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Macro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly MacroDbContext _context;

        public ItemsController(MacroDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            var items = await _context.Items.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Calories = i.Calories,
                Protein = i.Protein,
                Carbohydrates = i.Carbohydrates,
                Fiber = i.Fiber,
                Sugars = i.Sugars,
                Fat = i.Fat,
                ItemTypeId = i.ItemTypeId
            }).ToListAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(int id)
        {
            var item = await _context.Items.Select(i => new ItemDto
            {
                Id = i.Id,
                Name = i.Name,
                Calories = i.Calories,
                Protein = i.Protein,
                Carbohydrates = i.Carbohydrates,
                Fiber = i.Fiber,
                Sugars = i.Sugars,
                Fat = i.Fat,
                ItemTypeId = i.ItemTypeId
            }).FirstOrDefaultAsync(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> CreateItem(ItemDto itemDto)
        {
            var item = new Item
            {
                Name = itemDto.Name,
                Calories = itemDto.Calories,
                Protein = itemDto.Protein,
                Carbohydrates = itemDto.Carbohydrates,
                Fiber = itemDto.Fiber,
                Sugars = itemDto.Sugars,
                Fat = itemDto.Fat,
                ItemTypeId = itemDto.ItemTypeId
            };

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            itemDto.Id = item.Id;

            return CreatedAtAction(nameof(GetItem), new { id = itemDto.Id }, itemDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, ItemDto itemDto)
        {
            if (id != itemDto.Id)
            {
                return BadRequest();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            item.Name = itemDto.Name;
            item.Calories = itemDto.Calories;
            item.Protein = itemDto.Protein;
            item.Carbohydrates = itemDto.Carbohydrates;
            item.Fiber = itemDto.Fiber;
            item.Sugars = itemDto.Sugars;
            item.Fat = itemDto.Fat;
            item.ItemTypeId = itemDto.ItemTypeId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
