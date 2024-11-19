using DeltaboxAPI.Domain.Entities.DeltaBox.Banner;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Banner
{
    public interface IBannerService : IDisposable
    {
        Task<Result> CreateOrUpdateBranner(AdsBanner request);
    }
}
