using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodErp.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public float Purchase { get; set; }
        public StoreViewModel Store { get; set; }
    }
}