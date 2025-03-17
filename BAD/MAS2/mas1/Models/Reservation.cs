using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; }

        [ForeignKey("SharedExperience")]
        public int SharedExperienceId { get; set; }

        public SharedExperience SharedExperience { get; set; }
    }
}
