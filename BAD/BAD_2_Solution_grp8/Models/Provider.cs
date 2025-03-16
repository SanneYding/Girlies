namespace ExperienceAPI.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Experience> Experiences { get; set; } = new();
    }
}
