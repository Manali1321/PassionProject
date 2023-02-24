using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProject.Models.ViewModels
{
    public class UpdateOrder
    {
        public OrderDto SelectedOrder { get; set; }
        public IEnumerable<GroceryDto> GroceryOptions { get; set; }
        public IEnumerable<StoreDto> StoreOptions { get; set; }

    }
}