using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Order
{
    [Table("wishlist")]
    public class Wishlist : CommonEntity
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

        [Column("is_active")]
        public string? IsActive { get; set; }
    }
}
