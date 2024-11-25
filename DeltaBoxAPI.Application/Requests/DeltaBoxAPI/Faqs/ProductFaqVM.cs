using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs
{
    public class ProductFaqVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string IsActive { get; set; }
    }
}
