using System.ComponentModel.DataAnnotations;

namespace mas1.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Range(0, 100)]
        public double Amount { get; set; }
    }
}
