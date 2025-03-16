using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public ICollection<SharedExperience> SharedExperiences { get; set; }
    }
}