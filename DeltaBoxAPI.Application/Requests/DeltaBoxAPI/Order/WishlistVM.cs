﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public class WishlistVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int VariantId { get; set; }
        public string VariantName { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal DiscountAmount { get; set; }
        public string StockStatus { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
