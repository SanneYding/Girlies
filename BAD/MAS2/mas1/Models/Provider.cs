using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mas1.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Experience> Experiences { get; set; }
    }
}
