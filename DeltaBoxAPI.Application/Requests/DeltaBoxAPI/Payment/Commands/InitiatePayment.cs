using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands
{
    public class InitiatePayment : IRequest<UddoktaPaymentResponse>
    {
        public UddoktaPaymentRequest UddoktaPaymentRequest { get; set; }

        public InitiatePayment(UddoktaPaymentRequest uddoktaPaymentRequest)
        {
            UddoktaPaymentRequest = uddoktaPaymentRequest;
        }
    }

    public class InitiatePaymentHandler : IRequestHandler<InitiatePayment, UddoktaPaymentResponse>
    {
        private readonly IPaymentService _paymentService;

        public InitiatePaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<UddoktaPaymentResponse> Handle(InitiatePayment request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.CreatePaymentCharge(request.UddoktaPaymentRequest);
            return result;
        }
    }
}
