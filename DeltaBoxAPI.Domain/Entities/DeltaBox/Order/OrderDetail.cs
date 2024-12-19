using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Order
{
    [Table("order_details")]
    public class OrderDetail : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("order_profile_id")]
        public int OrderProfileId { get; set; }

        [Column("item_invoice_no")]
        public string? ItemInvoiceNo { get; set; }

        [Column("invoice_no")]
        public string? InvoiceNo { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("product_name")]
        public string? ProductName { get; set; }

        [Column("thumbnail_image")]
        public string? ThumbnailImage { get; set; }

        [Column("product_variant_id")]
        public int ProductVariantId { get; set; }

        [Column("product_variant_name")]
        public string? ProductVariantName { get; set; }

        [Column("sku")]
        public string? Sku { get; set; }

        [Column("unit_price")]
        public decimal? UnitPrice { get; set; }

        [Column("final_price")]
        public decimal? FinalPrice { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("sub_total")]
        public decimal? SubTotal { get; set; }

        [Column("total")]
        public decimal? Total { get; set; }

        [Column("order_item_status")]
        public string? OrderItemStatus { get; set; }
    }
}
