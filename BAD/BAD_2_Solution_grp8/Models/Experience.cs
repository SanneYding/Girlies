namespace ExperienceAPI.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public List<SharedExperience> SharedExperiences { get; set; } = new();
    }
}
