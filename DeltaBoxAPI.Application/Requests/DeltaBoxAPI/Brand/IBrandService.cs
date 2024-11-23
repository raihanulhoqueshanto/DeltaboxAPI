﻿using DeltaboxAPI.Domain.Entities.DeltaBox.Brand;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Brand
{
    public interface IBrandService : IDisposable
    {
        Task<Result> CreateOrUpdateBrand(AssociateBrand request);
    }
}
