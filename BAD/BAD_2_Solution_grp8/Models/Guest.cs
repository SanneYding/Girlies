namespace ExperienceAPI.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Reservation> Reservations { get; set; } = new();
    }
}
