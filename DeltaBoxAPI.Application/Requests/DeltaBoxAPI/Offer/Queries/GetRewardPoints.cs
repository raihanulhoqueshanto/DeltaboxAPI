using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Queries
{
    public class GetRewardPoints : IRequest<RewardPointVM>
    {
        public GetRewardPoints()
        {
            
        }
    }

    public class GetRewardPointsHandler : IRequestHandler<GetRewardPoints, RewardPointVM>
    {
        private readonly IOfferService _offerService;

        public GetRewardPointsHandler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<RewardPointVM> Handle(GetRewardPoints request, CancellationToken cancellationToken)
        {
            return await _offerService.GetRewardPoints();
        }
    }
}
