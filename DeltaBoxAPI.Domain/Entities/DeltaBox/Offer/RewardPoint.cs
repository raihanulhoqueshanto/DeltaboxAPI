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
    [Table("reward_point")]
    public class RewardPoint : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("customer_id")]
        public string? CustomerId { get; set; }

        [Column("point")]
        public decimal? Point { get; set; }

        [Column("redeemed_point")]
        public decimal? RedeemedPoint { get; set; }
    }
}
