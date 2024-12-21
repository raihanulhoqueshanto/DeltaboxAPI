using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Commands
{
    public class ApplyPromotionCode : IRequest<Result>
    {
        public PromotionCodeRequest PromotionCodeRequest { get; set; }

        public ApplyPromotionCode(PromotionCodeRequest promotionCodeRequest)
        {
            PromotionCodeRequest = promotionCodeRequest;
        }
    }

    public class ApplyPromotionCodeHandler : IRequestHandler<ApplyPromotionCode, Result>
    {
        private readonly IOfferService _offerService;

        public ApplyPromotionCodeHandler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<Result> Handle(ApplyPromotionCode request, CancellationToken cancellationToken)
        {
            var result = await _offerService.ApplyPromotionCode(request.PromotionCodeRequest);
            return result;
        }
    }
}
