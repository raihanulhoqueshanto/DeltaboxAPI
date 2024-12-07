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
        public string Name { get; set; }
        public string Slug { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public string StockStatus { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
