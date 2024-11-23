using DeltaboxAPI.Domain.Entities.DeltaBox.Brand;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand.Commands
{
    public class CreateOrUpdateBrand : IRequest<Result>
    {
        public AssociateBrand AssociateBrand { get; set; }

        public CreateOrUpdateBrand(AssociateBrand associateBrand)
        {
            AssociateBrand = associateBrand;
        }
    }

    public class CreateOrUpdateBrandHandler : IRequestHandler<CreateOrUpdateBrand, Result>
    {
        private readonly IBrandService _brandService;

        public CreateOrUpdateBrandHandler(IBrandService brandService)
        {
            _brandService = brandService ?? throw new ArgumentNullException(nameof(brandService));
        }

        public async Task<Result> Handle(CreateOrUpdateBrand request, CancellationToken cancellationToken)
        {
            var result = await _brandService.CreateOrUpdateBrand(request.AssociateBrand);
            return result;
        }
    }
}
