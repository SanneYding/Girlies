using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExperienceAPI.Data;
using ExperienceAPI.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace ExperienceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProviderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Provider
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
        {
            // Brug ToListAsync til at hente listen asynkront
            var providers = await _context.Providers.ToListAsync();
            return Ok(providers);
        }
    }
}
