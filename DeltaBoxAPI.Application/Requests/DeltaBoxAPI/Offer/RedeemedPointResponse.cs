using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer
{
    public class RedeemedPointResponse
    {
        public decimal CoinRedeemed { get; set; }
        public decimal SubTotal { get; set; }
        public decimal PromotionCodeAmount { get; set; }
        public decimal Total { get; set; }
    }
}
