using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mas1.Models;
using mas1.Data;

namespace mas1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedExperienceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SharedExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SharedExperience>>> GetSharedExperiences()
        {
            return await _context.SharedExperiences.Include(se => se.Experience).Include(se => se.Discount).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SharedExperience>> GetSharedExperience(int id)
        {
            var sharedExperience = await _context.SharedExperiences.FindAsync(id);

            if (sharedExperience == null)
            {
                return NotFound();
            }

            return sharedExperience;
        }

        [HttpPost]
        public async Task<ActionResult<SharedExperience>> PostSharedExperience(SharedExperience sharedExperience)
        {
            _context.SharedExperiences.Add(sharedExperience);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSharedExperience", new { id = sharedExperience.Id }, sharedExperience);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSharedExperience(int id, SharedExperience sharedExperience)
        {
            if (id != sharedExperience.Id)
            {
                return BadRequest();
            }

            _context.Entry(sharedExperience).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSharedExperience(int id)
        {
            var sharedExperience = await _context.SharedExperiences.FindAsync(id);
            if (sharedExperience == null)
            {
                return NotFound();
            }

            _context.SharedExperiences.Remove(sharedExperience);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
