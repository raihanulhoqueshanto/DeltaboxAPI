using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Faqs
{
    [Table("faqs_setup")]
    public class FaqsSetup
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
