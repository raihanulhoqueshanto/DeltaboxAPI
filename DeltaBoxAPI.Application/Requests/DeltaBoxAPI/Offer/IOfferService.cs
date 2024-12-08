using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer
{
    public interface IOfferService : IDisposable
    {
        Task<Result> CreateOrUpdatePromotionCode(PromotionCode request);
    }
}
