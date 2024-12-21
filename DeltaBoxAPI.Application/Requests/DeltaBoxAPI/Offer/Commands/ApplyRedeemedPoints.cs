using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Commands
{
    public class ApplyRedeemedPoints : IRequest<Result>
    {
        public RedeemedPointRequest RedeemedPointRequest { get; set; }

        public ApplyRedeemedPoints(RedeemedPointRequest redeemedPointRequest)
        {
            RedeemedPointRequest = redeemedPointRequest;
        }
    }

    public class ApplyRedeemedPointsHAndler : IRequestHandler<ApplyRedeemedPoints, Result>
    {
        private readonly IOfferService _offerService;

        public ApplyRedeemedPointsHAndler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<Result> Handle(ApplyRedeemedPoints request, CancellationToken cancellationToken)
        {
            var result = await _offerService.ApplyRedeemedPoints(request.RedeemedPointRequest);
            return result;
        }
    }
}
