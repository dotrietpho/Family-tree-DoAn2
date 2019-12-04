using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CayGiaPhaAPI.Class
{
    public class Node
    {
        [Key]
        public int Id { get; set; }
        public List<Person> persons { get; set; }
        public List<Node> childs { get; set; }
        public int? IdNodeCha { get; set; }

        public Node() { }
        public Node(int idCha)
        {
            IdNodeCha = idCha;
        }
    }
}