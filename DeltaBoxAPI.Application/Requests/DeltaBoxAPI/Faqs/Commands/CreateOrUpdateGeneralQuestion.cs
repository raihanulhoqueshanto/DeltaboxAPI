using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Commands
{
    public class CreateOrUpdateGeneralQuestion : IRequest<Result>
    {
        public GeneralQuestion GeneralQuestion { get; set; }

        public CreateOrUpdateGeneralQuestion(GeneralQuestion generalQuestion)
        {
            GeneralQuestion = generalQuestion;
        }
    }

    public class CreateOrUpdateGeneralQuestionHandler : IRequestHandler<CreateOrUpdateGeneralQuestion, Result>
    {
        private readonly IFaqsService _faqsService;

        public CreateOrUpdateGeneralQuestionHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<Result> Handle(CreateOrUpdateGeneralQuestion request, CancellationToken cancellationToken)
        {
            var result = await _faqsService.CreateOrUpdateGeneralQuestion(request.GeneralQuestion);
            return result;
        }
    }
}