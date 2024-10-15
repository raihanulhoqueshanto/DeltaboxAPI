using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Commands
{
    public class CreateOrUpdateProduct : IRequest<Result>
    {
        public CreateOrUpdateProductRequest ProductRequest { get; set; }

        public CreateOrUpdateProduct(CreateOrUpdateProductRequest productRequest)
        {
            ProductRequest = productRequest;
        }

        public class CreateOrUpdateProductHandler : IRequestHandler<CreateOrUpdateProduct, Result>
        {
            private readonly IProductService _productService;

            public CreateOrUpdateProductHandler(IProductService productService)
            {
                _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            }

            public async Task<Result> Handle(CreateOrUpdateProduct request, CancellationToken cancellationToken)
            {
                var result = await _productService.CreateOrUpdateProduct(request.ProductRequest);
                return result;
            }
        }
    }
}
