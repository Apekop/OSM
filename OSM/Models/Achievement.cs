﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OSM.Models
{
    public class Achievement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [MaxLength(100)]
        public string Naam { get; set; }
        [MaxLength(255)]
        public string Beschrijving { get; set; }
    }
}