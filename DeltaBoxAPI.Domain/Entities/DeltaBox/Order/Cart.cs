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
    [Table("cart")]
    public class Cart : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("customer_id")]
        public string? CustomerId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("sku")]
        public string Sku { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("is_active")]
        public string? IsActive { get; set; }
    }
}
