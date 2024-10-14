using DeltaBoxAPI.Application.Common.Models;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Domain.Entities.DeltaBox.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Commands
{
    public class CreateOrUpdateProductCategory : IRequest<Result>
    {
        public ProductCategory ProductCategory { get; set; }

        public CreateOrUpdateProductCategory(ProductCategory productCategory)
        {
            ProductCategory = productCategory;
        }
    }

    public class CreateOrUpdateProductCategoryHandler : IRequestHandler<CreateOrUpdateProductCategory, Result>
    {
        private readonly IProductService _productService;

        public CreateOrUpdateProductCategoryHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<Result> Handle(CreateOrUpdateProductCategory request, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateOrUpdateProductCategory(request.ProductCategory);
            return result;
        }
    }
}