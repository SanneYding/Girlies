using Microsoft.AspNetCore.Mvc;
using mas1.Models;

namespace mas1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExperiencesController : ControllerBase
    {
        private readonly List<Experience> _experiences = new()
        {
            new Experience { Name = "Beach Getaway", Description = "Relax on sunny beaches.", Price = 500 },
            new Experience { Name = "Mountain Hike", Description = "Explore scenic trails.", Price = 200 },
            new Experience { Name = "City Tour", Description = "Discover urban landmarks.", Price = 150 }
        };

        [HttpGet]
        public IActionResult GetExperience()
        {
            return Ok(_experiences);
        }
    }
}
