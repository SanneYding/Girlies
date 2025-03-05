using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Vaerksted.Models
{
    public class Faktura
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string? MekanikerNavn { get; set; } = string.Empty;

        [ForeignKey(typeof(Opgave))] // Link to Opgave
        public int? OpgaveID { get; set; }

        public double? Timer { get; set; }
        public float? Timepris { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Materialer> Materialer { get; set; } = [];

        [Ignore] // Not stored in SQLite, calculated field
        public float? TotalPris
        {
            get
            {
                double? v = (Timer * Timepris) + Materialer.Sum(static m => m.TotalPris);
                return (float)v;
            }
        }
    }
}
