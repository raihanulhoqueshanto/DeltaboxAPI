using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner.Queries
{
    public class GetCommonImage : PageParameters, IRequest<PagedList<CommonImageVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetCommonImage(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetCommonImageHandler : IRequestHandler<GetCommonImage, PagedList<CommonImageVM>>
    {
        private readonly IBannerService _bannerService;

        public GetCommonImageHandler(IBannerService bannerService)
        {
            _bannerService = bannerService ?? throw new ArgumentNullException(nameof(bannerService));
        }

        public async Task<PagedList<CommonImageVM>> Handle(GetCommonImage request, CancellationToken cancellationToken)
        {
            return await _bannerService.GetCommonImage(request);
        }
    }
}
