using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public class FilterProductVM
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public decimal Price { get; set; }
        public string StockStatus { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
