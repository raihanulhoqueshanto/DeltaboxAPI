using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public class CreateOrderRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PromotionCode { get; set; }
        public string OrderNote { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public decimal CoinRedeemed { get; set; }
        public List<OrderDetailRequest> Details { get; set; }
    }

    public class OrderDetailRequest
    {
        public int ProductId { get; set; }
        public int ProductVariantId { get; set; }
        public string Sku { get; set; }
    }
}
