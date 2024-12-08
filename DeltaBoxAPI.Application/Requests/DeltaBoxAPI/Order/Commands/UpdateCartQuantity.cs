using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands
{
    public class UpdateCartQuantity : IRequest<Result>
    {
        public UpdateCartQuantityRequest UpdateCartQuantityRequest { get; set; }

        public UpdateCartQuantity(UpdateCartQuantityRequest updateCartQuantityRequest)
        {
            UpdateCartQuantityRequest = updateCartQuantityRequest;
        }
    }

    public class UpdateCartQuantityHandler : IRequestHandler<UpdateCartQuantity, Result>
    {
        private readonly IOrderService _orderService;

        public UpdateCartQuantityHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(UpdateCartQuantity request, CancellationToken cancellationToken)
        {
            var result = await _orderService.UpdateCartQuantity(request.UpdateCartQuantityRequest);
            return result;
        }
    }
}
