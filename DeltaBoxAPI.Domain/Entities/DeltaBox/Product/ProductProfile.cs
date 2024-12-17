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
    [Table("product_profile")]
    public class ProductProfile : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("short_description")]
        public string? ShortDescription { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("thumbnail_image")]
        public string ThumbnailImage { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }

        [Column("latest_offer")]
        public string LatestOffer { get; set; }
    }
}
