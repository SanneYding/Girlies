using DAB_2_Solution_grp2.Models;
using System.Collections.Generic;

namespace ExperienceAPI.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}