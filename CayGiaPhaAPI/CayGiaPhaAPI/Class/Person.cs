using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CayGiaPhaAPI.Class
{
    public class Person
    {
        [Key]

        public int personId { get; set; }
        public string personName { get; set; }
        public string personGender { get; set; }
        public string personImg { get; set; }

        public int? IdNodeCha { get; set; }
    }
}