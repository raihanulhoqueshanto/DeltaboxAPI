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
    public class AddToCart : IRequest<Result>
    {
        public Cart Cart { get; set; }

        public AddToCart(Cart cart)
        {
            Cart = cart;
        }
    }

    public class AddToCartHandler : IRequestHandler<AddToCart, Result>
    {
        private readonly IOrderService _orderService;

        public AddToCartHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(AddToCart request, CancellationToken cancellationToken)
        {
            var result = await _orderService.AddToCart(request.Cart);
            return result;
        }
    }
}
