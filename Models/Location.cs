using System;
using System.Collections.Generic;

namespace FoodErp.Models
{
    public partial class Location
    {
        public Location()
        {
            Store = new HashSet<Store>();
        }

        public int LocationId { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Store> Store { get; set; }
    }
}
