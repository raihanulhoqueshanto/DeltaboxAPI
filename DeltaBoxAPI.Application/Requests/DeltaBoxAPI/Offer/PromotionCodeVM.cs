using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer
{
    public class PromotionCodeVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime PromotionStartDate { get; set; }
        public DateTime PromotionEndDate { get; set; }
        public string OneTime { get; set; }
        public string IsActive { get; set; }
    }
}
