using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
        public string IsActive { get; set; }
        public string LatestOffer { get; set; }
        public List<ColorwiseVariantVM> ColorGroups { get; set; }
    }

    public class ColorwiseVariantVM
    {
        public string ColorName { get; set; }
        public List<string> Images { get; set; }
        public List<ProductVariantVM> Variants { get; set; }
    }

    public class ProductVariantVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string SKU { get; set; }
        public decimal DpPrice { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public string IsActive { get; set; }
        public List<ProductAttributeVM> Attributes { get; set; }
    }

    public class ProductAttributeVM
    {
        public int Id { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public string IsActive { get; set; }
    }
}
