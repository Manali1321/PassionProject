using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models
{
    public class Grocery
    {
        [Key]
        public int ProductId { get; set; }
        public string Upc { get; set; }
        public string Name { get; set; }

        //Weight in grams
        public int Weight { get; set; }
        public int Stock { get; set; }

        //Orders can have many grocery items(One-To-Many)
        public ICollection<Order> Orders { get; set; }



    }
    public class GroceryDto
    { 
        public int ProductId { get; set; }
        public string Upc { get; set; }
        public string Name { get; set; }
    //Weight in grams
        public int Weight { get; set; }
        public int Stock { get; set; }
    }

}