﻿using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public interface IOrderService : IDisposable
    {
        Task<Result> AddInWishlist(Wishlist request);
        Task<Result> RemoveFromWishlist(RemoveFromWishlist request);
        Task<List<WishlistVM>> GetWishlist();
        Task<Result> AddToCart(Cart request);
        Task<Result> RemoveFromCart(RemoveFromCart request);
        Task<GetCartVM> GetCart();
        Task<Result> UpdateCartQuantity(UpdateCartQuantityRequest request);
        Task<Result> CreateOrder(CreateOrderRequest request);
        Task<PagedList<OrderVM>> GetOrder(GetOrder request);
    }
}
