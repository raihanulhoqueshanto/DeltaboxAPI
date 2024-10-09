using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Domain.Entities.DeltaBox.Faqs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries
{
    public class GetFaqs : PageParameters, IRequest<PagedList<FaqsVM>>
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetFaqs(int? id, string? title, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Title = title;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetFaqsHandler : IRequestHandler<GetFaqs, PagedList<FaqsVM>>
    {
        private readonly IFaqsService _faqsService;

        public GetFaqsHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<PagedList<FaqsVM>> Handle(GetFaqs request, CancellationToken cancellationToken)
        {
            return await _faqsService.GetFaqs(request);
        }
    }
}
