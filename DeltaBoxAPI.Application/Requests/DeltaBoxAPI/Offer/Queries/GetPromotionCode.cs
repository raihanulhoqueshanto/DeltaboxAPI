using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Queries
{
    public class GetPromotionCode : PageParameters, IRequest<PagedList<PromotionCodeVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetPromotionCode(int? id, string? name, string? code, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            Code = code;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetPromotionCodeHandler : IRequestHandler<GetPromotionCode, PagedList<PromotionCodeVM>>
    {
        private readonly IOfferService _offerService;

        public GetPromotionCodeHandler(IOfferService offerService)
        {
            _offerService = offerService ?? throw new ArgumentNullException(nameof(offerService));
        }

        public async Task<PagedList<PromotionCodeVM>> Handle(GetPromotionCode request, CancellationToken cancellationToken)
        {
            return await _offerService.GetPromotionCode(request);
        }
    }
}
