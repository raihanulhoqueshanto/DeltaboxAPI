using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Blog
{
    [Table("article")]
    public class Article : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("writer_name")]
        public string WriterName { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("image")]
        public string Image { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("short_description")]
        public string? ShortDescription { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
