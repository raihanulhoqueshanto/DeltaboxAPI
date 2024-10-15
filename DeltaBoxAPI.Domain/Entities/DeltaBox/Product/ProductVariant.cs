using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Product
{
    [Table("product_variant")]
    public class ProductVariant : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("sku")]
        public string SKU { get; set; }

        [Column("dp_price")]
        public decimal DPPrice { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock_quantity")]
        public int StockQuantity { get; set; }

        [Column("discount_amount")]
        public decimal DiscountAmount { get; set; }

        [Column("discount_start_date")]
        public DateTime? DiscountStartDate { get; set; }

        [Column("discount_end_date")]
        public DateTime? DiscountEndDate { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
