using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class LandHistorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Manager Manager { get; set; }
        public Land Land { get; set; }
        public int CompetitieGewonnen { get; set; }
        public int BekerGewonnen { get; set; }
        public int DoelstellingBehaald { get; set; }
    }
}