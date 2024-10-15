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
    [Table("product_image")]
    public class ProductImage : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("variant_id")]
        public int VariantId { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
