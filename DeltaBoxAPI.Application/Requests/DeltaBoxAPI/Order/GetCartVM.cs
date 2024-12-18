using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public class GetCartVM
    {
        public decimal SubTotal { get; set; }
        public List<CartVM> Carts { get; set; }
    }

    public class CartVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public string Sku { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public string StockStatus { get; set; }
        public string ThumbnailImage { get; set; }
    }

    public class UpdateCartQuantityRequest
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
    }
}
