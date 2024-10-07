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
    public class CreateOrUpdateFaqs : IRequest<Result>
    {
        public FaqsSetup FaqsSetup { get; set; }

        public CreateOrUpdateFaqs(FaqsSetup faqsSetup)
        {
            FaqsSetup = faqsSetup;
        }
    }

    public class CreateOrUpdateFaqsHandler : IRequestHandler<CreateOrUpdateFaqs, Result>
    {
        private readonly IFaqsService _faqsService;

        public CreateOrUpdateFaqsHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<Result> Handle(CreateOrUpdateFaqs request, CancellationToken cancellationToken)
        {
            var result = await _faqsService.CreateOrUpdateFaqs(request.FaqsSetup);
            return result;
        }
    }
}
