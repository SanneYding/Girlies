using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace Vaerksted.Models
{
    public class Opgave
    {
        [PrimaryKey, AutoIncrement]
        public Guid ID { get; set; }

        public string? CustomerName { get; set; } = string.Empty;
        public string? CustomerAddress { get; set; } = string.Empty;
        public string? CarMake { get; set; } = string.Empty;
        public string? CarModel { get; set; } = string.Empty;
        public string? CarLicense { get; set; } = string.Empty;
        public DateTime? Date { get; set; } = DateTime.Now;
        public string? Work { get; set; } = string.Empty;

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Faktura> Fakturaer { get; set; } = [];
    }
}
