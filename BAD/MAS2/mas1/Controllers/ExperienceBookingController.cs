using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mas1.Models;
using mas1.Data;

namespace mas1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceBookingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExperienceBookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExperienceBooking>>> GetExperienceBookings()
        {
            return await _context.ExperienceBookings
                                 .Include(eb => eb.Experience)
                                 .Include(eb => eb.Reservation)
                                 .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExperienceBooking>> GetExperienceBooking(int id)
        {
            var experienceBooking = await _context.ExperienceBookings
                                                   .Include(eb => eb.Experience)
                                                   .Include(eb => eb.Reservation)
                                                   .FirstOrDefaultAsync(eb => eb.Id == id);

            if (experienceBooking == null)
            {
                return NotFound();
            }

            return experienceBooking;
        }

        [HttpPost]
        public async Task<ActionResult<ExperienceBooking>> PostExperienceBooking(ExperienceBooking experienceBooking)
        {
            _context.ExperienceBookings.Add(experienceBooking);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperienceBooking", new { id = experienceBooking.Id }, experienceBooking);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperienceBooking(int id, ExperienceBooking experienceBooking)
        {
            if (id != experienceBooking.Id)
            {
                return BadRequest();
            }

            _context.Entry(experienceBooking).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperienceBooking(int id)
        {
            var experienceBooking = await _context.ExperienceBookings.FindAsync(id);
            if (experienceBooking == null)
            {
                return NotFound();
            }

            _context.ExperienceBookings.Remove(experienceBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
