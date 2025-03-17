using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class ExperienceBooking
    {
        [Key]
        public int ExperienceBookingID { get; set; }

        [ForeignKey("Experience")]
        public int ExperienceID { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationID { get; set; }

        [ForeignKey("Guest")]
        public int GuestID { get; set; }

        public Experience Experience { get; set; }
        public Reservation Reservation { get; set; }

        public Guest Guest { get; set; }
    }
}
