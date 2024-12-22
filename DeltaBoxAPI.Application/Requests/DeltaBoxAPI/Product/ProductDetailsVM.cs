using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Slug { get; set; }
        public decimal Rating { get; set; }
        public int ReviewCount { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
        public List<ProductDetailsVariant> Variants { get; set; }
        public List<string> Images { get; set; }
    }

    public class ProductDetailsVariant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sku { get; set; }
        public string StockStatus { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
    }
}
