using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Payment;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment
{
    public interface IPaymentService : IDisposable
    {
        Task<Result> CreateOrUpdatePaymentProof(PaymentProof request);
        Task<PagedList<PaymentProofVM>> GetPaymentProof(GetPaymentProof request);
    }
}
