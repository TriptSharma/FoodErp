using System;
using System.Collections.Generic;

namespace FoodErp.Models
{
    public partial class Store
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public int LocationId { get; set; }
        public double? Revenue { get; set; }

        public virtual Location Location { get; set; }
    }
}
