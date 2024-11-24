using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public class FilterOptionVM
    {
        public List<FilterAvailability> Availability { get; set; }
        public FilterPrice Price { get; set; }
        public List<FilterCategory> Categories { get; set; }
        public FilterSubscription Subscriptions { get; set; }
    }

    public class FilterAvailability
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class FilterPrice
    {
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }

    public class FilterCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FilterSubscription
    {
        public List<FilterPlan> Plan { get; set; }
        public List<FilterDuration> Duration { get; set; }
    }

    public class FilterPlan
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class FilterDuration
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
