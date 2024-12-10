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
    public class CreateOrUpdateCommonImage : IRequest<Result>
    {
        public CommonImage CommonImage { get; set; }

        public CreateOrUpdateCommonImage(CommonImage commonImage)
        {
            CommonImage = commonImage;
        }
    }

    public class CreateOrUpdateCommonImageHandler : IRequestHandler<CreateOrUpdateCommonImage, Result>
    {
        private readonly IBannerService _bannerService;

        public CreateOrUpdateCommonImageHandler(IBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        public async Task<Result> Handle(CreateOrUpdateCommonImage request, CancellationToken cancellationToken)
        {
            var result = await _bannerService.CreateOrUpdateCommonImage(request.CommonImage);
            return result;
        }
    }
}
