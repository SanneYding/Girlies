using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class ExperienceBooking
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Experience")]
        public int ExperienceId { get; set; }

        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        public Experience Experience { get; set; }
        public Reservation Reservation { get; set; }
    }
}
