using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Faqs.Queries
{
    public class GetProductFaq : PageParameters, IRequest<PagedList<ProductFaqVM>>
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? Keyword { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetProductFaq(int? id, int? productId, string? keyword, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            ProductId = productId;
            Keyword = keyword;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetProductFaqHandler : IRequestHandler<GetProductFaq, PagedList<ProductFaqVM>>
    {
        private readonly IFaqsService _faqsService;

        public GetProductFaqHandler(IFaqsService faqsService)
        {
            _faqsService = faqsService ?? throw new ArgumentNullException(nameof(faqsService));
        }

        public async Task<PagedList<ProductFaqVM>> Handle(GetProductFaq request, CancellationToken cancellationToken)
        {
            return await _faqsService.GetProductFaq(request);
        }
    }
}
