using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Commands
{
    public class CreateOrUpdatePromotionCode : IRequest<Result>
    {
        public PromotionCode PromotionCode { get; set; }

        public CreateOrUpdatePromotionCode(PromotionCode promotionCode)
        {
            PromotionCode = promotionCode;
        }
    }

    public class CreateOrUpdatePromotionCodeHandler : IRequestHandler<CreateOrUpdatePromotionCode, Result>
    {
        private readonly IOfferService _offerService;

        public CreateOrUpdatePromotionCodeHandler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<Result> Handle(CreateOrUpdatePromotionCode request, CancellationToken cancellationToken)
        {
            var result = await _offerService.CreateOrUpdatePromotionCode(request.PromotionCode);
            return result;
        }
    }
}
