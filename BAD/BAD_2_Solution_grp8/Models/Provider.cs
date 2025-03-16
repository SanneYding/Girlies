using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Experience> Experiences { get; set; }
    }
}