using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Queries
{
    public class GetWishlist : IRequest<List<WishlistVM>>
    {
        public GetWishlist()
        {
            
        }
    }

    public class GetWishlistHandler : IRequestHandler<GetWishlist, List<WishlistVM>>
    {
        private readonly IOrderService _orderService;

        public GetWishlistHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<List<WishlistVM>> Handle(GetWishlist request, CancellationToken cancellationToken)
        {
            return await _orderService.GetWishlist();
        }
    }
}
