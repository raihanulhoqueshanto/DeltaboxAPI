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
    public class CreateOrUpdateProductReview : IRequest<Result>
    {
        public ProductReview ProductReview { get; set; }

        public CreateOrUpdateProductReview(ProductReview productReview)
        {
            ProductReview = productReview;
        }
    }

    public class CreateOrUpdateProductReviewHandler : IRequestHandler<CreateOrUpdateProductReview, Result>
    {
        private readonly IFaqsService _faqsService;

        public CreateOrUpdateProductReviewHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<Result> Handle(CreateOrUpdateProductReview request, CancellationToken cancellationToken)
        {
            var result = await _faqsService.CreateOrUpdateProductReview(request.ProductReview);
            return result;
        }
    }
}
