using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands
{
    public class AddInWishlist : IRequest<Result>
    {
        public Wishlist Wishlist { get; set; }

        public AddInWishlist(Wishlist wishlist)
        {
            Wishlist = wishlist;
        }
    }

    public class AddToWishlistHandler : IRequestHandler<AddInWishlist, Result>
    {
        private readonly IOrderService _orderService;

        public AddToWishlistHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(AddInWishlist request, CancellationToken cancellationToken)
        {
            var result = await _orderService.AddInWishlist(request.Wishlist);
            return result;
        }
    }
}
