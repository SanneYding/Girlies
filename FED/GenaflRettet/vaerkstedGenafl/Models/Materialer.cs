using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vaerkstedGenafl.Models
{
    public class Materialer
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int FakturaID { get; set; } // Foreign key til Faktura

        public string Navn { get; set; }

        public float Pris { get; set; }
    }
}
