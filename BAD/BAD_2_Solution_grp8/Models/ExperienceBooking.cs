namespace ExperienceAPI.Models
{
    public class ExperienceBooking
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public List<Discount> Discounts { get; set; } = new();
    }
}
