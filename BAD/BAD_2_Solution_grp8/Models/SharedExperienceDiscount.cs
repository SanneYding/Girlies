namespace ExperienceAPI.Models
{
    public class SharedExperienceDiscount
    {
        public int SharedExperienceId { get; set; }
        public SharedExperience SharedExperience { get; set; }
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }
    }
}