using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<SharedExperienceDiscount> SharedExperienceDiscounts { get; set; }
        public ICollection<ExperienceBookingDiscount> ExperienceBookingDiscounts { get; set; }
    }
}