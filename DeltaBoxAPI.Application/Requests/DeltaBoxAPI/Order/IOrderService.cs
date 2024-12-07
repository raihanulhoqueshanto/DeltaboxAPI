using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using DeltaBoxAPI.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order
{
    public interface IOrderService : IDisposable
    {
        Task<Result> AddInWishlist(Wishlist request);
    }
}
