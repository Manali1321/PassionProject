using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProject.Models
{
    public class Store
    {
        [Key]
        public int StoreID { get; set; }

        public string Name { get; set; }

        //store having many order
        public ICollection<Order> Orders { get; set; }
    }
    public class StoreDto
    {
        public int StoreID { get; set; }

        public string Name { get; set; }
    }
}
        