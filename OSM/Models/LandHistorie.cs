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

        // De combinatie Manager-Land moet uniek zijn
        //[Index("IX_Unique_Land_Manager", 1, IsUnique = true)] // Het lukt nog even niet met de unique index...
        public Manager Manager { get; set; }
        //[Index("IX_Unique_Land_Manager", 2, IsUnique = true)]
        public Land Land { get; set; }
        public int CompetitieGewonnen { get; set; }
        public int BekerGewonnen { get; set; }
        public int DoelstellingBehaald { get; set; }
    }
}