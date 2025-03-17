using System.ComponentModel.DataAnnotations;

namespace mas1.Models
{
    public class Discount
    {
        [Key]
        public int DiscountID { get; set; }

        [Required]
        public int GroupSize { get; set; }

        [Range(0, 100)]
        public int DiscountPercentage { get; set; }
    }
}
