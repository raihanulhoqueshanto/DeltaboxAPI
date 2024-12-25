using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Blog
{
    public class ArticleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string WriterName { get; set; }
        public int CategoryId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string IsActive { get; set; }
    }
}
