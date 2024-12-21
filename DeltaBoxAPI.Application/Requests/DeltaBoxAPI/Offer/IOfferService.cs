using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Offer.Queries;
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
        Task<PagedList<PromotionCodeVM>> GetPromotionCode(GetPromotionCode request);
        Task<RewardPointVM> GetRewardPoints();
        Task<Result> ApplyPromotionCode(PromotionCodeRequest request);
        Task<Result> ApplyRedeemedPoints(RedeemedPointRequest request);
    }
}
