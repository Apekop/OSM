using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class Historie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Datum { get; set; }

        public virtual Team Team1 { get; set; }
        public virtual Team Team2 { get; set; }

        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }
}