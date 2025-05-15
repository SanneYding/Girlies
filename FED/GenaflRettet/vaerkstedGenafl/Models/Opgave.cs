using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaerkstedGenafl.Models
{
    public class Opgave
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string KundeNavn { get; set; }
        public string Adresse { get; set; }
        public string BilMaerke { get; set; }
        public string BilModel { get; set; }
        public string Registreringsnummer { get; set; }
        public DateTime Indlevering { get; set; }
        public string Beskrivelse { get; set; }
    }
}
