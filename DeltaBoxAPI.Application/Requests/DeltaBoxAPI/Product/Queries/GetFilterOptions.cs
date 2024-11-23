using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries
{
    public class GetFilterOptions : IRequest<FilterOptionVM>
    {
        public GetFilterOptions()
        {
            
        }
    }

    public class GetFilterOptionsHandler : IRequestHandler<GetFilterOptions, FilterOptionVM>
    {
        private readonly IProductService _productService;

        public GetFilterOptionsHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<FilterOptionVM> Handle(GetFilterOptions request, CancellationToken cancellationToken)
        {
            return await _productService.GetFilterOptions(request);
        }
    }
}
