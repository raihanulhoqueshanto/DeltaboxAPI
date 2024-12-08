using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Queries
{
    public class GetCart : IRequest<GetCartVM>
    {
        public GetCart()
        {
            
        }
    }

    public class GetCartHandler : IRequestHandler<GetCart, GetCartVM>
    {
        private readonly IOrderService _orderService;

        public GetCartHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<GetCartVM> Handle(GetCart request, CancellationToken cancellationToken)
        {
            return await _orderService.GetCart();
        }
    }
}
