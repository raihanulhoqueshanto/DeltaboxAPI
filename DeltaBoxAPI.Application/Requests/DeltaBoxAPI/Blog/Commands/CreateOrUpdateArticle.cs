using DeltaboxAPI.Domain.Entities.DeltaBox.Blog;
using DeltaBoxAPI.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Blog.Commands
{
    public class CreateOrUpdateArticle : IRequest<Result>
    {
        public Article Article { get; set; }

        public CreateOrUpdateArticle(Article article)
        {
            Article = article;
        }
    }

    public class CreateOrUpdateArticleHandler : IRequestHandler<CreateOrUpdateArticle, Result>
    {
        private readonly IBlogService _blogService;

        public CreateOrUpdateArticleHandler(IBlogService blogService)
        {
            _blogService = blogService ?? throw new ArgumentNullException(nameof(blogService));
        }

        public async Task<Result> Handle(CreateOrUpdateArticle request, CancellationToken cancellationToken)
        {
            var result = await _blogService.CreateOrUpdateArticle(request.Article);
            return result;
        }
    }
}
