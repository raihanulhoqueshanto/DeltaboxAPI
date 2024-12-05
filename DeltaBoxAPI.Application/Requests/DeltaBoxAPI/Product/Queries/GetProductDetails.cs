using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries
{
    public class GetProductDetails : IRequest<ProductDetailsVM>
    {
        public int? Id { get; set; }

        public GetProductDetails(int? id)
        {
            Id = id;
        }
    }

    public class GetProductDetailsHandler : IRequestHandler<GetProductDetails, ProductDetailsVM>
    {
        private readonly IProductService _productService;

        public GetProductDetailsHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<ProductDetailsVM> Handle(GetProductDetails request, CancellationToken cancellationToken)
        {
            return await _productService.GetProductDetails(request);
        }
    }
}
