using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public ICollection<SharedExperience> SharedExperiences { get; set; }
        public ICollection<ExperienceBooking> ExperienceBookings { get; set; }
    }
}