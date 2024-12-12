using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands
{
    public class CreateOrder : IRequest<Result>
    {
        public CreateOrderRequest CreateOrderRequest { get; set; }

        public CreateOrder(CreateOrderRequest createOrderRequest)
        {
            CreateOrderRequest = createOrderRequest;
        }
    }

    public class CreateOrderHandler : IRequestHandler<CreateOrder, Result>
    {
        private readonly IOrderService _orderService;

        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<Result> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var result = await _orderService.CreateOrder(request.CreateOrderRequest);
            return result;
        }
    }
}
