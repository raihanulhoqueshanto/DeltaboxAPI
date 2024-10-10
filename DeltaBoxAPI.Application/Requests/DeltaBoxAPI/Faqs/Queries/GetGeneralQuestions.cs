using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs;
using MediatR;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries
{
    public class GetGeneralQuestions : PageParameters, IRequest<PagedList<GeneralQuestionVM>>
    {
        public int? Id { get; set; }
        public string? Question { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetGeneralQuestions(int? id, string? question, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Question = question;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetGeneralQuestionsHandler : IRequestHandler<GetGeneralQuestions, PagedList<GeneralQuestionVM>>
    {
        private readonly IFaqsService _faqsService;

        public GetGeneralQuestionsHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<PagedList<GeneralQuestionVM>> Handle(GetGeneralQuestions request, CancellationToken cancellationToken)
        {
            return await _faqsService.GetGeneralQuestions(request);
        }
    }
}