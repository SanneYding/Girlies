using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mas1.Models
{
    public class Provider
    {
        [Key]
        public int ProviderID { get; set; }

        public string? BusinessAddress { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CVR { get; set; }

        public virtual ICollection<Experience> Experiences { get; set; }
    }
}
