using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands
{
    public class VerifyPayment : IRequest<bool>
    {
        public string InvoiceId { get; set; }

        public VerifyPayment(string invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }

    public class VerifyPaymentHandler : IRequestHandler<VerifyPayment, bool>
    {
        private readonly IPaymentService _paymentService;

        public VerifyPaymentHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        public async Task<bool> Handle(VerifyPayment request, CancellationToken cancellationToken)
        {
            var result = await _paymentService.VerifyPayment(request.InvoiceId);
            return result;
        }
    }
}
