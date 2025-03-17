using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class Experience
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Provider")]
        public int ProviderId { get; set; }

        public Provider Provider { get; set; }

        public ICollection<SharedExperience> SharedExperiences { get; set; }

        public ICollection<ExperienceBooking> ExperienceBookings { get; set; }
    }
}
