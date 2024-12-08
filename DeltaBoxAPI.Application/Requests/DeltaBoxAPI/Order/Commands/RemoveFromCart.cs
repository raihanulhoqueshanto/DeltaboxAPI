using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands
{
    public class RemoveFromCart : IRequest<Result>
    {
        public int Id { get; set; }

        public RemoveFromCart(int id)
        {
            Id = id;
        }
    }

    public class RemoveFromCartHandler : IRequestHandler<RemoveFromCart, Result>
    {
        private readonly IOrderService _orderService;

        public RemoveFromCartHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(RemoveFromCart request, CancellationToken cancellationToken)
        {
            var result = await _orderService.RemoveFromCart(request);
            return result;
        }
    }
}
