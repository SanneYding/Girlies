namespace ExperienceAPI.Models
{
    public class SharedExperience
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }
        public ICollection<SharedExperienceDiscount> SharedExperienceDiscounts { get; set; }
    }
}