using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CayGiaPhaAPI.Class
{
    public class PersonRelationship
    {
        [Key]
        [Column(Order = 1)]
        public int personId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int toPersonId { get; set; }
        public string Ralationship { get; set; }
    }
}