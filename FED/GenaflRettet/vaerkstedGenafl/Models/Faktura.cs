using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaerkstedGenafl.Models
{
    public class Faktura
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int OpgaveID { get; set; } // Foreign key til Opgave

        public string Mekaniker { get; set; }

        public float Timer { get; set; }

        public float TimePris { get; set; }

        public DateTime Dato { get; set; } = DateTime.Now;
    }
}