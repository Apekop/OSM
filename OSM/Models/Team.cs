using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(255)]
        public string Naam { get; set; }
        public int Doelstelling { get; set; }
        public virtual Competitie Competitie { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual List<Speler> Spelers { get; set; }
        public virtual List<Historie> Type { get; set; }
    }
}