using DeltaboxAPI.Domain.Entities.DeltaBox.Banner;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner.Commands
{
    public class CreateOrUpdateBranner : IRequest<Result>
    {
        public AdsBanner AdsBanner { get; set; }

        public CreateOrUpdateBranner(AdsBanner adsBanner)
        {
            AdsBanner = adsBanner;
        }
    }

    public class CreateOrUpdateBrannerHandler : IRequestHandler<CreateOrUpdateBranner, Result>
    {
        private readonly IBannerService _bannerService;

        public CreateOrUpdateBrannerHandler(IBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        public async Task<Result> Handle(CreateOrUpdateBranner request, CancellationToken cancellationToken)
        {
            var result = await _bannerService.CreateOrUpdateBranner(request.AdsBanner);
            return result;
        }
    }
}
