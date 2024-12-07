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
        public string? CategoryId { get; set; }
        public string? LatestOffer { get; set; }
        public string? Stock { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Plan { get; set; }
        public string? Duration { get; set; }
        public string? SortBy { get; set; }
        public string? GetAll { get; set; }

        public GetFilterProducts(string? keyword, int? id, string? categoryId, string? latestOffer, string? stock, decimal? minPrice, decimal? maxPrice, string? plan, string? duration, string? sortBy, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Keyword = keyword;
            Id = id;
            CategoryId = categoryId;
            LatestOffer = latestOffer;
            Stock = stock;
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Plan = plan;
            Duration = duration;
            SortBy = sortBy;
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