using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands
{
    public class RemoveFromWishlist : IRequest<Result>
    {
        public int Id { get; set; }

        public RemoveFromWishlist(int id)
        {
            Id = id;
        }
    }

    public class RemoveFromWishlistHandler : IRequestHandler<RemoveFromWishlist, Result>
    {
        private readonly IOrderService _orderService;

        public RemoveFromWishlistHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(RemoveFromWishlist request, CancellationToken cancellationToken)
        {
            var result = await _orderService.RemoveFromWishlist(request);
            return result;
        }
    }
}
