using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands
{
    public class CreateOrUpdatePaymentProof : IRequest<Result>
    {
        public PaymentProof PaymentProof { get; set; }

        public CreateOrUpdatePaymentProof(PaymentProof paymentProof)
        {
            PaymentProof = paymentProof;
        }
    }

    public class CreateOrUpdatePaymentProofHandler : IRequestHandler<CreateOrUpdatePaymentProof, Result>
    {
        private readonly IPaymentService _paymentService;

        public CreateOrUpdatePaymentProofHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<Result> Handle(CreateOrUpdatePaymentProof request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.CreateOrUpdatePaymentProof(request.PaymentProof);
            return result;
        }
    }
}
