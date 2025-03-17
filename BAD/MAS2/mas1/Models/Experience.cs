using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mas1.Models
{
    public class Experience
    {
        [Key]
        public int ExperienceID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string? Description { get; set; }

        [ForeignKey("Provider")]
        public int ProviderID { get; set; }

        [JsonIgnore]
        public virtual Provider? Provider { get; set; }
    }
}
