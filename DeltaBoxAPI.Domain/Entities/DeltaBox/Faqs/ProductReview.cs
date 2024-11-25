using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Faqs
{
    [Table("product_review")]
    public class ProductReview : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("customer_name")]
        public string CustomerName { get; set; }

        [Column("customer_email")]
        public string CustomerEmail { get; set; }

        [Column("review")]
        public string Review { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
