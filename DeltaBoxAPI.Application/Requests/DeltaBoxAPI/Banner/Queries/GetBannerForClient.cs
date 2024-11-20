using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner.Queries
{
    public class GetBannerForClient : IRequest<List<List<AdsBannerVM>>>
    {
        public string? Section { get; set; }
        public string? Type { get; set; }

        public GetBannerForClient(string? section, string? type)
        {
            Section = section;
            Type = type;
        }
    }

    public class GetBannerForClientHandler : IRequestHandler<GetBannerForClient, List<List<AdsBannerVM>>>
    {
        private readonly IBannerService _bannerService;

        public GetBannerForClientHandler(IBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        public async Task<List<List<AdsBannerVM>>> Handle(GetBannerForClient request, CancellationToken cancellationToken)
        {
            return await _bannerService.GetBannerForClient(request);
        }
    }
}
