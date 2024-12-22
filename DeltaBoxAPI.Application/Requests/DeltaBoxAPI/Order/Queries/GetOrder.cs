using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Queries
{
    public class GetOrder : PageParameters, IRequest<PagedList<OrderVM>>
    {
        public int? Id { get; set; }
        public string? Keyword { get; set; }
        public string? InvoiceNo { get; set; }
        public string? ItemInvoiceNo { get; set; }
        public string? OrderStatus { get; set; }
        public string? OrderItemStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status { get; set; }
        public string? GetAll { get; set; }

        public GetOrder(int? id, string? keyword, string? invoiceNo, string? itemInvoiceNo, string? orderStatus, string? orderItemStatus, string? paymentMethod, string? status, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Keyword = keyword;
            InvoiceNo = invoiceNo;
            ItemInvoiceNo = itemInvoiceNo;
            OrderStatus = orderStatus;
            OrderItemStatus = orderItemStatus;
            PaymentMethod = paymentMethod;
            Status = status;
            GetAll = getAll;
        }
    }

    public class GetOrderHandler : IRequestHandler<GetOrder, PagedList<OrderVM>>
    {
        private readonly IOrderService _orderService;

        public GetOrderHandler(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<PagedList<OrderVM>> Handle(GetOrder request, CancellationToken cancellationToken)
        {
            return await _orderService.GetOrder(request);
        }
    }
}
