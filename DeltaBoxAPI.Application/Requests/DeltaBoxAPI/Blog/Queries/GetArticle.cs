using DeltaboxAPI.Application.Common.Pagings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Blog.Queries
{
    public class GetArticle : PageParameters, IRequest<PagedList<ArticleVM>>
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? IsActive { get; set; }
        public string? GetAll { get; set; }

        public GetArticle(int? id, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage) : base(currentPage, itemsPerPage)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            GetAll = getAll;
        }
    }

    public class GetArticleHandler : IRequestHandler<GetArticle, PagedList<ArticleVM>>
    {
        private readonly IBlogService _blogService;

        public GetArticleHandler(IBlogService blogService)
        {
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
        }

        public async Task<PagedList<ArticleVM>> Handle(GetArticle request, CancellationToken cancellationToken)
        {
            return await _blogService.GetArticle(request);
        }
    }
}
