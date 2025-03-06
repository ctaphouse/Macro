using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Macro.Api.Data; // Replace with your actual namespace
using Macro.Api.Models; // Replace with your actual namespace
using Macro.Shared.Dtos;

namespace Macro.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemTypesController : ControllerBase
    {
        private readonly BullFrogDbContext _context;

        private readonly IMapper _mapper;

        public ItemTypesController(BullFrogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/itemtype)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemTypeDto>>> GetItemTypes()
        {
            var itemtypes = await _context.ItemTypes.OrderBy(i => i.Name).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<ItemTypeDto>>(itemtypes));
        }

        // GET: api/itemtype/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemTypeDto>> GetItemType(int id)
        {
            var itemtype = await _context.ItemTypes.FindAsync(id);
            if (itemtype == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ItemTypeDto>(itemtype));
        }

        // POST: api/itemtype
        [HttpPost]
        public async Task<ActionResult<ItemTypeDto>> CreateItemType(ItemTypeDto itemtypeDto)
        {
            var itemtype = _mapper.Map<ItemType>(itemtypeDto);
            _context.ItemTypes.Add(itemtype);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetItemType), new { id = itemtype.Id }, _mapper.Map<ItemTypeDto>(itemtype));
        }

        // PUT: api/itemtype/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemType(int id, ItemTypeDto itemtypeDto)
        {
            if (id != itemtypeDto.Id)
            {
                return BadRequest();
            }

            var itemtype = await _context.ItemTypes.FindAsync(id);
            if (itemtype == null)
            {
                return NotFound();
            }

            _mapper.Map(itemtypeDto, itemtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/itemtype/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemType(int id)
        {
            var itemtype = await _context.ItemTypes.FindAsync(id);
            if (itemtype == null)
            {
                return NotFound();
            }

            _context.ItemTypes.Remove(itemtype);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
