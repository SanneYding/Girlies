namespace ExperienceAPI.Models
{
    public class SharedExperience
    {
        public int Id { get; set; }
        public int ExperienceId { get; set; }
        public Experience Experience { get; set; }

        public List<Discount> Discounts { get; set; } = new();
        public List<Reservation> Reservations { get; set; } = new();
    }
}
