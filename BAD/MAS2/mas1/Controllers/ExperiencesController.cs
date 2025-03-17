using mas1.Controllers;
using mas1.Data;
using mas1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mas1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExperienceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Experience
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Experience>>> GetExperiences()
        {
            // This will return all experiences with full provider data
            return await _context.Experiences
                                 .Include(e => e.Provider)  // This includes the related Provider entity
                                 .ToListAsync();
        }

        // GET: api/Experience/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Experience>> GetExperience(int id)
        {
            var experience = await _context.Experiences
                                           .Include(e => e.Provider)  // This includes the related Provider entity
                                           .FirstOrDefaultAsync(e => e.ExperienceID == id);

            if (experience == null)
            {
                return NotFound();
            }

            return experience;
        }

        [HttpPost]
        public async Task<ActionResult<Experience>> PostExperience(Experience experience)
        {
            // Only add the Experience with the provided ProviderID
            _context.Experiences.Add(experience);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExperience", new { id = experience.ExperienceID }, experience);
        }


        // PUT: api/Experience/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExperience(int id, Experience experience)
        {
            if (id != experience.ExperienceID)
            {
                return BadRequest();
            }

            _context.Entry(experience).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Experience/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }

            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
