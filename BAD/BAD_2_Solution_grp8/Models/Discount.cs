namespace ExperienceAPI.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public List<SharedExperience> SharedExperiences { get; set; } = new();
        public List<ExperienceBooking> ExperienceBookings { get; set; } = new();
    }
}
