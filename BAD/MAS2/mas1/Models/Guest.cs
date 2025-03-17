using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mas1.Models
{
    public class Guest
    {
        [Key]
        public int GuestID { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
