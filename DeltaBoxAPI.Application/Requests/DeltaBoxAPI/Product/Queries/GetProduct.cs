using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries
{
    public class GetProduct : PageParameters, IRequest<PagedList<ProductVM>>
    {
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetProduct(int? id, int? categoryId, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            CategoryId = categoryId;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetProductHandler : IRequestHandler<GetProduct, PagedList<ProductVM>>
    {
        private readonly IProductService _productService;

        public GetProductHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<PagedList<ProductVM>> Handle(GetProduct request, CancellationToken cancellationToken)
        {
            return await _productService.GetProduct(request);
        }
    }
}
