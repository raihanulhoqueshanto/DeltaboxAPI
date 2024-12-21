using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Commands
{
    public class ApplyRedeemedPoints : IRequest<RedeemedPointResponse>
    {
        public RedeemedPointRequest RedeemedPointRequest { get; set; }

        public ApplyRedeemedPoints(RedeemedPointRequest redeemedPointRequest)
        {
            RedeemedPointRequest = redeemedPointRequest;
        }
    }

    public class ApplyRedeemedPointsHAndler : IRequestHandler<ApplyRedeemedPoints, RedeemedPointResponse>
    {
        private readonly IOfferService _offerService;

        public ApplyRedeemedPointsHAndler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<RedeemedPointResponse> Handle(ApplyRedeemedPoints request, CancellationToken cancellationToken)
        {
            var result = await _offerService.ApplyRedeemedPoints(request.RedeemedPointRequest);
            return result;
        }
    }
}
