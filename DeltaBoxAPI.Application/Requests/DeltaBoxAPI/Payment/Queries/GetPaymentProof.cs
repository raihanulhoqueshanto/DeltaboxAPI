using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Queries
{
    public class GetPaymentProof : PageParameters, IRequest<PagedList<PaymentProofVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetPaymentProof(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetPaymentProofHandler : IRequestHandler<GetPaymentProof, PagedList<PaymentProofVM>>
    {
        private readonly IPaymentService _paymentService;

        public GetPaymentProofHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<PagedList<PaymentProofVM>> Handle(GetPaymentProof request, CancellationToken cancellationToken)
        {
            return await _paymentService.GetPaymentProof(request);
        }
    }
}
