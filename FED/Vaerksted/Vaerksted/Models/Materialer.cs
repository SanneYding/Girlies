using SQLite;
using SQLiteNetExtensions.Attributes;

namespace Vaerksted.Models
{
    public class Materialer
    {
        [PrimaryKey, AutoIncrement]
        public int? ID { get; set; }

        public string? Navn { get; set; } = string.Empty;
        public float? Pris { get; set; }
        public int? Antal { get; set; }

        [Ignore] // Not stored in database, calculated field
        public float? TotalPris => Pris * Antal;

        [ForeignKey(typeof(Faktura))]
        public int? FakturaID { get; set; }

        [ManyToOne]
        public Faktura? Faktura { get; set; }
    }
}
