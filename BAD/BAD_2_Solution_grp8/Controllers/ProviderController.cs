using ExperienceAPI.Data;            
using ExperienceAPI.Models;          
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class ProviderController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProviderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Provider>>> GetProviders()
    {
        return await _context.Providers.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Provider>> GetProvider(int id)
    {
        var provider = await _context.Providers.FindAsync(id);

        if (provider == null)
        {
            return NotFound();
        }

        return provider;
    }
}
