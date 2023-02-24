using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PassionProject.Controllers;
using PassionProject.Models.ViewModels;


namespace PassionProject.Models.ViewModels
{
    public class MyViewModel
    {
        public IEnumerable<StoreDto> StoreOptions { get; set; }
        public IEnumerable<GroceryDto> GroceryOptions { get; set; }
    }
}