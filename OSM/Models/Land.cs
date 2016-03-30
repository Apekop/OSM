using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class Land
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naam { get; set; }
        [MaxLength(2)]
        [Index("Iso_Unique", IsUnique = true)]
        public string IsoCode { get; set; }
        public Continent Continent { get; set; }
        public bool Beschikbaar { get; set; }
    }
    
    public enum Continent
    {
        NoordAmerika,
        ZuidAmerika,
        Europa,
        Afrika,
        Azië,
        Australië
    }
}