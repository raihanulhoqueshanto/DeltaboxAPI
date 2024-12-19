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
    [Table("order_profile")]
    public class OrderProfile : CommonEntity
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("invoice_no")]
        public string? InvoiceNo { get; set; }

        [Column("customer_id")]
        public string? CustomerId { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone_number")]
        public string? PhoneNumber { get; set; }

        [Column("net_amount")]
        public decimal? NetAmount { get; set; }

        [Column("sub_total")]
        public decimal? SubTotal { get; set; }

        [Column("total")]
        public decimal? Total { get; set; }

        [Column("coin_redeemed")]
        public decimal? CoinRedeemed { get; set; }

        [Column("promotion_code")]
        public string? PromotionCode { get; set; }

        [Column("promotion_code_amount")]
        public decimal? PromotionCodeAmount { get; set; }

        [Column("no_of_use")]
        public int? NoOfUse { get; set; }

        [Column("order_status")]
        public string? OrderStatus { get; set; }

        [Column("order_note")]
        public string? OrderNote { get; set; }
    }
}
