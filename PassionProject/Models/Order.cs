using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PassionProject.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int Quantity { get; set; }

        //store has many order
        [ForeignKey("Store")]
        public int StoreID { get; set; }
        public virtual Store Store { get; set; }

        //each order has many grocery
        [ForeignKey("Grocery")]
        public int ProductId { get; set; }
        public virtual Grocery Grocery { get; set; }

    }
    public class OrderDto
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public int Quantity { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }

    }
}