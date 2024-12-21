using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer
{
    public class PromotionCodeRequest
    {
        public string PromotionCode { get; set; }
        public decimal SubTotal { get; set; }
        public decimal CoinRedeemed { get; set; }
    }
}
