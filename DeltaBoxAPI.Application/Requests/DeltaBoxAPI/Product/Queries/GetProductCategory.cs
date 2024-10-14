using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries
{
    public class GetProductCategory : PageParameters, IRequest<PagedList<ProductCategoryVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetProductCategory(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetProductCategoryHandler : IRequestHandler<GetProductCategory, PagedList<ProductCategoryVM>>
    {
        private readonly IProductService _productService;

        public GetProductCategoryHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<PagedList<ProductCategoryVM>> Handle(GetProductCategory request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductCategory(request);
        }
    }
}
