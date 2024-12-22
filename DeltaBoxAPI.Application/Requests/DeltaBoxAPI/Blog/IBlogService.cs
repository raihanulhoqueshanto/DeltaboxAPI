using DeltaboxAPI.Domain.Entities.DeltaBox.Blog;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Blog
{
    public interface IBlogService : IDisposable
    {
        Task<Result> CreateOrUpdateArticle(Article request);
    }
}
