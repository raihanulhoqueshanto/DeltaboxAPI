﻿using DeltaBoxAPI.Application.Common.Models;
using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Domain.Entities.DeltaBox.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product
{
    public interface IProductService : IDisposable
    {
        Task<Result> CreateOrUpdateProductCategory(ProductCategory request);
        Task<PagedList<ProductCategoryVM>> GetProductCategory(GetProductCategory request);
    }
}