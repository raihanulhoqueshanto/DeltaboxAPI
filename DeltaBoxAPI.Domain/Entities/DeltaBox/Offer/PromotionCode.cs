using DeltaboxAPI.Domain.Entities.DeltaBox.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Domain.Entities.DeltaBox.Offer
{
    [Table("promotion_code")]
    public class PromotionCode : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        [Column("promotion_start_date")]
        public DateTime PromotionStartDate { get; set; }

        [Column("promotion_end_date")]
        public DateTime PromotionEndDate { get; set; }

        [Column("one_time")]
        public string OneTime { get; set; }

        [Column("is_active")]
        public string IsActive { get; set; }
    }
}
