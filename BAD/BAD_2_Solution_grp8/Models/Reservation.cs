namespace ExperienceAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }

        public List<SharedExperience> SharedExperiences { get; set; } = new();
        public List<ExperienceBooking> ExperienceBookings { get; set; } = new();
    }
}
