using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class ExperienceBooking
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public ICollection<ExperienceBookingDiscount> ExperienceBookingDiscounts { get; set; }
    }
}