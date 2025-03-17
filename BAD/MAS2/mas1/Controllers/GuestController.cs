using mas1.Data;
using mas1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mas1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
            private readonly ApplicationDbContext _context;

            public GuestController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Guest>>> GetGuest()
            {
                return await _context.Guest.ToListAsync();
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Guest>> GetGuest(int id)
            {
                var guest = await _context.Guest.FindAsync(id);

                if (guest == null)
                {
                    return NotFound();
                }

                return guest;
            }

            [HttpPost]
            public async Task<ActionResult<Guest>> PostGuest(Guest guest)
            {
                _context.Guest.Add(guest);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGuest", new { id = guest.GuestID }, guest);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> PutGuest(int id, Guest guest)
            {
                if (id != guest.GuestID)
                {
                    return BadRequest();
                }

                _context.Entry(guest).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteGuest(int id)
            {
                var guest = await _context.Guest.FindAsync(id);
                if (guest == null)
                {
                    return NotFound();
                }

                _context.Guest.Remove(guest);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
