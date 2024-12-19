using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string InvoiceNo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public decimal NetAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal? CoinRedeemed { get; set; }
        public string? PromotionCode { get; set; }
        public decimal? PromotionCodeAmount { get; set; }
        public string OrderStatus { get; set; }
        public string? OrderNote { get; set; }
        public string PaymentMethod { get; set; }
        public string SenderNumber { get; set; }
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }
        public string Fee { get; set; }
        public string ChargedAmount { get; set; }
    }
}
