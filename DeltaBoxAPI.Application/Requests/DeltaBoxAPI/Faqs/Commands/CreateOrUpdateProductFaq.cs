using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands
{
    public class CreateOrUpdateProductFaq : IRequest<Result>
    {
        public ProductFaq ProductFaq { get; set; }

        public CreateOrUpdateProductFaq(ProductFaq productFaq)
        {
            ProductFaq = productFaq;
        }
    }

    public class CreateProductFaqHandler : IRequestHandler<CreateOrUpdateProductFaq, Result>
    {
        private readonly IFaqsService _faqsService;

        public CreateProductFaqHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<Result> Handle(CreateOrUpdateProductFaq request, CancellationToken cancellationToken)
        {
            var result = await _faqsService.CreateOrUpdateProductFaq(request.ProductFaq);
            return result;
        }
    }
}
