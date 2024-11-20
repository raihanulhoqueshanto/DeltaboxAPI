using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaboxAPI.Application.Common.Pagings;
using MediatR;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries
{
    public class GetFilterProducts : PageParameters, IRequest<PagedList<FilterProductVM>>
    {
        public string? Keyword { get; set; }
        public int? Id { get; set; }
        public int? CategoryId { get; set; }
        public string? GetAll { get; set; }

        public GetFilterProducts(string? keyword, int? id, int? categoryId, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Keyword = keyword;
            Id = id;
            CategoryId = categoryId;
            GetAll = getAll;
        }
    }

    public class GetFilterProductsHandler : IRequestHandler<GetFilterProducts, PagedList<FilterProductVM>>
    {
        private readonly IProductService _productService;

        public GetFilterProductsHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<PagedList<FilterProductVM>> Handle(GetFilterProducts request, CancellationToken cancellationToken)
        {
            return await _productService.GetFilterProducts(request);
        }
    }
}
