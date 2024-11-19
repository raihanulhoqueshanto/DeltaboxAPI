using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner.Queries
{
    public class GetBanner : PageParameters, IRequest<PagedList<AdsBannerVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Section { get; set; }
        public string? Type { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetBanner(int? id, string? name, string? section, string? type, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            Section = section;
            Type = type;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetBannerHandler : IRequestHandler<GetBanner, PagedList<AdsBannerVM>>
    {
        private readonly IBannerService _bannerService;

        public GetBannerHandler(IBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        public async Task<PagedList<AdsBannerVM>> Handle(GetBanner request, CancellationToken cancellationToken)
        {
            return await _bannerService.GetBanner(request);
        }
    }
}
