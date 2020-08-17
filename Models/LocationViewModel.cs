using System.Collections.Generic;

namespace FoodErp.Models
{
    public class LocationViewModel
    {
        public int LocationId { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public virtual ICollection<StoreViewModel> Stores { get; set; }
    }
}