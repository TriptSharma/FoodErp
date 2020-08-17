using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodErp.Models
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public Nullable<double> Revenue { get; set; }

        public LocationViewModel Location { get; set; }
    }
}