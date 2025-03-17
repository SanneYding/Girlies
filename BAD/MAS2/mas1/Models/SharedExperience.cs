using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class SharedExperience
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Experience")]
        public int ExperienceId { get; set; }

        [ForeignKey("Discount")]
        public int DiscountId { get; set; }

        public Experience Experience { get; set; }
        public Discount Discount { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
