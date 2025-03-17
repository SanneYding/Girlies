using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationID { get; set; }

        [ForeignKey("SharedExperience")]
        public int SharedExperienceID { get; set; }

        [ForeignKey("Guest")]
        public int GuestID { get; set; }

        public SharedExperience SharedExperience { get; set; }

        public Guest Guest { get; set; }
    }
}
