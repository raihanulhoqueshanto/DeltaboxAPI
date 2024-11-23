using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand.Queries
{
    public class GetBrand : PageParameters, IRequest<PagedList<BrandVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetBrand(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetBrandHandler : IRequestHandler<GetBrand, PagedList<BrandVM>>
    {
        private readonly IBrandService _brandService;

        public GetBrandHandler(IBrandService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        public async Task<PagedList<BrandVM>> Handle(GetBrand request, CancellationToken cancellationToken)
        {
            return await _brandService.GetBrand(request);
        }
    }
}
