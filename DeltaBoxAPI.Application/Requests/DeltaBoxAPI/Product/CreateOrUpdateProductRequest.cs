using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public class CreateOrUpdateProductRequest
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
        public string IsActive { get; set; }
        [Required]
        public List<ColorwiseVariant> ColorGroups { get; set; }
    }

    public class ColorwiseVariant
    {
        public string ColorName { get; set; } // e.g., "Black" / "White"
        public List<string>? Images { get; set; }
        public List<ProductVariantRequest> Variants { get; set; }
    }

    public class ProductVariantRequest
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required] 
        public int CategoryId { get; set; }
        [Required]
        public string SKU { get; set; }
        [Required]
        public decimal DpPrice { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int StockQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime? DiscountStartDate { get; set; }
        public DateTime? DiscountEndDate { get; set; }
        public string IsActive { get; set; }
        public List<ProductAttributeRequest> Attributes { get; set; }
    }

    public class ProductAttributeRequest
    {
        public int? Id { get; set; }
        [Required]
        public string AttributeName { get; set; }
        [Required]
        public string AttributeValue { get; set; }
        public string IsActive { get; set; }
    }
}