using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(255)]
        public string Naam { get; set; }

        // Het virtual keyword wordt in het Entity Framework gebruikt zodat lazy loading kan worden toegepast
        public virtual List<ManagerAchievement> Achievements { get; set; }
        public virtual List<ManagerVriend> Vrienden { get; set; }
        public virtual List<LandHistorie> Geschiedenis { get; set; } 
    }
}