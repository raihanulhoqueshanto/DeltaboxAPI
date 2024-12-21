using DeltaboxAPI.Application.Common.Interfaces;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Order.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Payment;
using DeltaboxAPI.Domain.Entities.DeltaBox.Offer;
using DeltaboxAPI.Domain.Entities.DeltaBox.Order;
using DeltaBoxAPI.Application.Common.Models;
using DeltaBoxAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaboxAPI.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly MysqlDbContext _mysqlContext;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPaymentService _paymentService;

        public OrderService(ApplicationDbContext context, MysqlDbContext mysqlContext, IHostEnvironment hostEnvironment, ICurrentUserService currentUserService, IPaymentService paymentService)
        {
            _context = context;
            _mysqlContext = mysqlContext;
            _hostEnvironment = hostEnvironment;
            _currentUserService = currentUserService;
            _paymentService = paymentService;
        }

        public void Dispose()
        {
            _context.Dispose();
            _mysqlContext.Dispose();
        }

        public async Task<Result> AddInWishlist(Wishlist request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var existingWishlist = await _context.Wishlists.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == request.ProductId && c.Sku.Replace(" ", "").ToLower() == request.Sku.Replace(" ", "").ToLower() && c.IsActive == "Y");

                if (existingWishlist != null)
                {
                    return Result.Failure("Failed", "409", new[] { "Already added in wishlist." }, null);
                }

                request.CustomerId = customerId;
                request.IsActive = "Y";
                await _context.Wishlists.AddAsync(request);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Added in wishlist." }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<Result> RemoveFromWishlist(RemoveFromWishlist request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var wishlistObj = await _context.Wishlists.FirstOrDefaultAsync(c => c.Id == request.Id && c.CustomerId == customerId && c.IsActive == "Y");
                if (wishlistObj != null)
                {
                    wishlistObj.IsActive = "N";
                }
                else
                {
                    return Result.Failure("Failed", "404", new[] { "Not found in wishlist." }, null);
                }

                 _context.Wishlists.Update(wishlistObj);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Removed Successfully" }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<List<WishlistVM>> GetWishlist()
        {
            var customerId = _currentUserService.UserId;
            var currentDate = DateTime.Now;

            var wishlistQuery = await (from w in _context.Wishlists
                                       join pp in _context.ProductProfiles on w.ProductId equals pp.Id
                                       join pv in _context.ProductVariants on w.Sku equals pv.SKU
                                       where w.CustomerId == customerId
                                             && w.IsActive == "Y"
                                             && pp.IsActive == "Y"
                                             && pv.IsActive == "Y"
                                       select new WishlistVM
                                       {
                                           Id = w.Id,
                                           ProductId = pp.Id,
                                           ProductName = pp.Name,
                                           VariantId = pv.Id,
                                           VariantName = pv.Name,
                                           Sku = pv.SKU,
                                           Price = pv.Price,
                                           FinalPrice = (pv.Price -
                                               (currentDate >= pv.DiscountStartDate && currentDate <= pv.DiscountEndDate
                                                   ? pv.DiscountAmount
                                                   : 0m)),
                                           DiscountAmount = currentDate >= pv.DiscountStartDate && currentDate <= pv.DiscountEndDate
                                                   ? pv.DiscountAmount
                                                   : 0m,
                                           StockStatus = pv.StockQuantity > 0 ? "In Stock" : "Out of Stock",
                                           ThumbnailImage = pp.ThumbnailImage
                                       }).ToListAsync();

            return wishlistQuery;
        }

        public async Task<Result> AddToCart(Cart request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var existingCart = await _context.Carts.FirstOrDefaultAsync(c => c.CustomerId == customerId && c.ProductId == request.ProductId && c.Sku.Replace(" ", "").ToLower() == request.Sku.Replace(" ", "").ToLower() && c.IsActive == "Y");

                if (existingCart != null)
                {
                    return Result.Failure("Failed", "409", new[] { "Already added in cart." }, null);
                }

                request.CustomerId = customerId;
                request.IsActive = "Y";
                await _context.Carts.AddAsync(request);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Added to cart." }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<Result> RemoveFromCart(RemoveFromCart request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var cartObj = await _context.Carts.FirstOrDefaultAsync(c => c.Id == request.Id && c.CustomerId == customerId && c.IsActive == "Y");
                if (cartObj != null)
                {
                    cartObj.IsActive = "N";
                }
                else
                {
                    return Result.Failure("Failed", "404", new[] { "Not found in cart." }, null);
                }

                _context.Carts.Update(cartObj);

                int result = await _context.SaveChangesAsync();

                return result > 0
                     ? Result.Success("Success", "200", new[] { "Removed Successfully" }, null)
                     : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);

            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<GetCartVM> GetCart()
        {
            var customerId = _currentUserService.UserId;
            var currentDate = DateTime.Now;

            var cartQuery = await(from c in _context.Carts
                                      join pp in _context.ProductProfiles on c.ProductId equals pp.Id
                                      join pv in _context.ProductVariants on c.Sku equals pv.SKU
                                      where c.CustomerId == customerId
                                            && c.IsActive == "Y"
                                            && pp.IsActive == "Y"
                                            && pv.IsActive == "Y"
                                      select new CartVM
                                      {
                                          Id = c.Id,
                                          ProductId = pp.Id,
                                          ProductName = pp.Name,
                                          VariantId = pv.Id,
                                          VariantName = pv.Name,
                                          Sku = pv.SKU,
                                          Quantity = c.Quantity,
                                          Price = pv.Price * c.Quantity,
                                          FinalPrice = (pv.Price -
                                              (currentDate >= pv.DiscountStartDate && currentDate <= pv.DiscountEndDate
                                                  ? pv.DiscountAmount
                                                  : 0m)) * c.Quantity,
                                          DiscountAmount = currentDate >= pv.DiscountStartDate && currentDate <= pv.DiscountEndDate
                                                  ? pv.DiscountAmount
                                                  : 0m,
                                          StockStatus = pv.StockQuantity > 0 ? "In Stock" : "Out of Stock",
                                          ThumbnailImage = pp.ThumbnailImage
                                      }).ToListAsync();

            // Ensure cartQuery is not null or empty
            var carts = cartQuery ?? new List<CartVM>();

            // Calculate subtotal safely, ensuring FinalPrice is valid
            decimal subTotal = carts.Sum(cart => cart.FinalPrice);

            // Create GetCartVM object
            var getCartVM = new GetCartVM
            {
                SubTotal = subTotal,
                Carts = carts
            };

            return getCartVM;
        }

        public async Task<Result> UpdateCartQuantity(UpdateCartQuantityRequest request)
        {
            try
            {
                var customerId = _currentUserService.UserId;
                var cartObj = await _context.Carts.FirstOrDefaultAsync(c => c.Id == request.Id && c.CustomerId == customerId && c.IsActive == "Y");

                if (cartObj != null)
                {
                    var productObj = await _context.ProductVariants.FirstOrDefaultAsync(c => c.ProductId == cartObj.ProductId && c.SKU == cartObj.Sku && c.IsActive == "Y");

                    if (productObj != null)
                    {
                        if(request.Quantity <= productObj.StockQuantity)
                        {
                            cartObj.Quantity = request.Quantity;
                        }
                        else
                        {
                            return Result.Failure("Failed", "500", new[] { "Cart quantity cannot be greater than stock quantity." }, null);
                        }
                    }

                    _context.Carts.Update(cartObj);

                    int result = await _context.SaveChangesAsync();

                    return result > 0
                         ? Result.Success("Success", "200", new[] { "Cart quantity updated successfully." }, null)
                         : Result.Failure("Failed", "500", new[] { "Operation failed. Please try again!" }, null);
                }
                else
                {
                    return Result.Failure("Failed", "404", new[] { "Not found in cart." }, null);
                }
            }
            catch (Exception ex)
            {
                var errorMessage = ex.InnerException?.Message ?? ex.Message;
                return Result.Failure("Failed", "500", new[] { errorMessage }, null);
            }
        }

        public async Task<Result> CreateOrder(CreateOrderRequest request)
        {
            // Use the database's execution strategy
            var strategy = _context.Database.CreateExecutionStrategy();

            return await strategy.ExecuteAsync(async () =>
            {
                // Wrap the entire operation in a transaction
                using var transaction = await _context.Database.BeginTransactionAsync();

                try
                {
                    // Get current user ID
                    var customerId = _currentUserService.UserId;

                    // Generate invoice number
                    var invoiceNo = await GenerateInvoiceNumber();

                    // Calculate promotion code amount
                    decimal promotionCodeAmount = 0;
                    if (!string.IsNullOrEmpty(request.PromotionCode))
                    {
                        promotionCodeAmount = await CalculatePromotionCodeAmount(request.PromotionCode);
                    }

                    // Create order profile
                    var orderProfile = await CreateOrderProfile(
                        customerId,
                        invoiceNo,
                        request,
                        promotionCodeAmount
                    );

                    // Process order details & retrieved redemeed point
                    var redeemedPoint = await CreateOrderDetails(orderProfile, request, promotionCodeAmount);

                    // Update reward points
                    await ProcessRewardPoints(customerId, orderProfile.Total, redeemedPoint);

                    // Update product variant stocks
                    await UpdateProductVariantStocks(request);

                    // Update cart
                    if(request.Type.Trim().ToLower() == "cart")
                    {
                        await UpdateCart(customerId);
                    }

                    // Save all changes
                    await _context.SaveChangesAsync();

                    // Commit transaction
                    await transaction.CommitAsync();

                    //OrderResponse orderResponse = new OrderResponse()
                    //{
                    //    FullName = $"{orderProfile.FirstName} {orderProfile.LastName}".Trim(), // Ensures proper spacing
                    //    Email = orderProfile.Email,
                    //    Amount = orderProfile.Total,
                    //    CustomerId = orderProfile.CustomerId,
                    //    Id = orderProfile.Id,
                    //    InvoiceNo = orderProfile.InvoiceNo
                    //};

                    //return Result.Success("Success", "200", new[] { "Order created successfully." }, orderResponse);

                    //Prepare UddoktaPaymentRequest and call CreatePaymentCharge
                    var paymentRequest = new UddoktaPaymentRequest
                    {
                        FullName = orderProfile.Name.Trim(),
                        Email = orderProfile.Email,
                        Amount = orderProfile.Total.ToString(),
                        Metadata = new Dictionary<string, string>
                        {
                            { "invoice_no", orderProfile.InvoiceNo },
                            { "customer_id", orderProfile.CustomerId }
                        },
                        RedirectUrl = "https://deltaboxit.vercel.app/order/success",
                        CancelUrl = "https://deltaboxit.vercel.app/order/cancel",
                        WebhookUrl = ""
                    };

                    var paymentResponse = await _paymentService.CreatePaymentCharge(paymentRequest);

                    //Add payment response to the final result
                    return Result.Success("Order and payment charge created successfully.", "200",
                        new[] { "Order and payment initiated successfully." },
                        new {Payment = paymentResponse});
                }
                catch (Exception ex)
                {
                    // Rollback transaction
                    await transaction.RollbackAsync();

                    var errorMessage = ex.InnerException?.Message ?? ex.Message;
                    return Result.Failure("Failed", "500", new[] { errorMessage }, null);
                }
            });
        }

        private async Task<string> GenerateInvoiceNumber()
        {
            // Generate invoice number like DIT202412120001
            var today = DateTime.Now.ToString("yyyyMMdd");
            var lastInvoice = await _context.OrderProfiles
                .Where(o => o.InvoiceNo.StartsWith($"DIT{today}"))
                .OrderByDescending(o => o.InvoiceNo)
                .FirstOrDefaultAsync();

            string newInvoiceNo;
            if (lastInvoice == null)
            {
                newInvoiceNo = $"DIT{today}0001";
            }
            else
            {
                var lastNumber = int.Parse(lastInvoice.InvoiceNo.Substring(11));
                newInvoiceNo = $"DIT{today}{(lastNumber + 1):D4}";
            }

            return newInvoiceNo;
        }

        private async Task<decimal> CalculatePromotionCodeAmount(string promotionCode)
        {
            if (string.IsNullOrWhiteSpace(promotionCode)) return 0;

            var now = DateTime.Now;
            var promoCodeEntity = await _context.PromotionCodes
                .FirstOrDefaultAsync(p =>
                    p.Code == promotionCode &&
                    p.IsActive == "Y" &&
                    p.PromotionStartDate <= now &&
                    p.PromotionEndDate >= now
                );

            return promoCodeEntity?.Amount ?? 0;
        }

        private async Task<OrderProfile> CreateOrderProfile(
            string customerId,
            string invoiceNo,
            CreateOrderRequest request,
            decimal promotionCodeAmount)
        {
            // Check promotion code usage
            int noOfUse = 0;
            if (!string.IsNullOrEmpty(request.PromotionCode))
            {
                noOfUse = await _context.OrderProfiles
                    .CountAsync(o =>
                    o.CustomerId == customerId.ToString() &&
                    o.PromotionCode == request.PromotionCode
                    ) + 1;
            }       

            var orderProfile = new OrderProfile
            {
                InvoiceNo = invoiceNo,
                CustomerId = customerId.ToString(),
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                PromotionCode = request.PromotionCode,
                PromotionCodeAmount = promotionCodeAmount,
                OrderNote = request.OrderNote,
                OrderStatus = "Order Placed",
                NoOfUse = noOfUse
            };

            await _context.OrderProfiles.AddAsync(orderProfile);
            await _context.SaveChangesAsync(); // Save to get the generated ID

            return orderProfile;
        }

        private async Task<decimal> CreateOrderDetails(OrderProfile orderProfile, CreateOrderRequest request, decimal? promotionCodeAmount)
        {
            var orderDetails = new List<OrderDetail>();
            decimal? netAmount = 0;
            decimal? subTotal = 0;

            foreach (var detail in request.Details)
            {
                // Fetch product and variant details
                var product = await _context.ProductProfiles
                    .FirstOrDefaultAsync(p => p.Id == detail.ProductId);

                var variant = await _context.ProductVariants
                    .FirstOrDefaultAsync(v =>
                        v.Id == detail.ProductVariantId &&
                        v.ProductId == detail.ProductId &&
                        v.SKU == detail.Sku
                    );

                if (product == null || variant == null)
                    throw new Exception("Invalid product or variant");

                // Calculate prices with potential discounts
                var now = DateTime.Now;
                decimal unitPrice = variant.Price;
                decimal discountAmount = variant.DiscountAmount > 0 &&
                    variant.DiscountStartDate <= now &&
                    variant.DiscountEndDate >= now
                    ? variant.DiscountAmount
                    : 0;
                decimal finalPrice = unitPrice - discountAmount;

                var itemInvoiceNo = await GenerateItemInvoiceNumber(orderProfile.InvoiceNo);

                var orderDetail = new OrderDetail
                {
                    OrderProfileId = orderProfile.Id,
                    ItemInvoiceNo = itemInvoiceNo,
                    InvoiceNo = orderProfile.InvoiceNo,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ThumbnailImage = product.ThumbnailImage,
                    ProductVariantId = variant.Id,
                    ProductVariantName = variant.Name,
                    Sku = variant.SKU,
                    UnitPrice = unitPrice,
                    FinalPrice = finalPrice,
                    Quantity = detail.Quantity,
                    SubTotal = unitPrice * detail.Quantity,
                    Total = finalPrice * detail.Quantity,
                    OrderItemStatus = "Order Placed"
                };

                orderDetails.Add(orderDetail);

                // Calculate net amount and subtotal
                netAmount += orderDetail.SubTotal;
                subTotal += orderDetail.Total;
            }

            // Add order details to context
            await _context.OrderDetails.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();

            // Update OrderProfile with calculated amounts
            var redeemedPoint = await UpdateOrderProfileAmounts(orderProfile, netAmount, subTotal, request, promotionCodeAmount);

            return redeemedPoint;
        }

        private async Task<decimal> UpdateOrderProfileAmounts(
            OrderProfile orderProfile,
            decimal? netAmount,
            decimal? subTotal,
            CreateOrderRequest request,
            decimal? promotionCodeAmount)
        {
            // Calculate coin redeemed (if any)
            decimal coinRedeemed = 0;

            var customerId = _currentUserService.UserId;
            var rewardPointObj = await _context.RewardPoints.FirstOrDefaultAsync(c => c.CustomerId == customerId);
            
            if(rewardPointObj != null)
            {
                if(request.CoinRedeemed >  0)
                {
                    if(request.CoinRedeemed > (rewardPointObj.Point - rewardPointObj.RedeemedPoint))
                    {
                        coinRedeemed = (rewardPointObj.Point - rewardPointObj.RedeemedPoint) ?? 0;
                    }
                    else
                    {
                        coinRedeemed = request.CoinRedeemed ?? 0;
                    }
                }
            }
            else
            {
                coinRedeemed = 0;
            }

            // Calculate final total
            decimal? total = subTotal - coinRedeemed - promotionCodeAmount;

            if(total < 0)
            {
                total = 0;
            }

            // Update OrderProfile
            orderProfile.NetAmount = netAmount;
            orderProfile.SubTotal = subTotal;
            orderProfile.CoinRedeemed = coinRedeemed;
            orderProfile.Total = total;

            // Update in context
            _context.OrderProfiles.Update(orderProfile);
            await _context.SaveChangesAsync();

            return coinRedeemed;
        }

        private async Task<string> GenerateItemInvoiceNumber(string parentInvoiceNo)
        {
            // Generate a truly unique item invoice number
            var today = DateTime.Now.ToString("yyyyMMdd");
            var uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
            var newItemInvoiceNo = $"ITM{today}{uniqueSuffix}";

            // Optional: Ensure uniqueness by checking against existing entries
            while (await _context.OrderDetails.AnyAsync(od => od.ItemInvoiceNo == newItemInvoiceNo))
            {
                uniqueSuffix = Guid.NewGuid().ToString("N").Substring(0, 6);
                newItemInvoiceNo = $"ITM{today}{uniqueSuffix}";
            }

            return newItemInvoiceNo;
        }

        private async Task ProcessRewardPoints(string customerId, decimal? total, decimal? redeemedPoint)
        {
            var rewardPoints = Math.Round((total ?? 0) / 10);

            var existingReward = await _context.RewardPoints
                .FirstOrDefaultAsync(r => r.CustomerId == customerId);

            if (existingReward == null)
            {
                var newReward = new RewardPoint
                {
                    CustomerId = customerId,
                    Point = rewardPoints,
                    RedeemedPoint = 0
                };
                await _context.RewardPoints.AddAsync(newReward);
            }
            else
            {
                existingReward.Point += rewardPoints;
                existingReward.RedeemedPoint += redeemedPoint;
            }

            await _context.SaveChangesAsync();
        }

        private async Task UpdateCart(string customerId)
        {
            // Perform a batch update directly in the database
            await _context.Carts
                .Where(c => c.CustomerId == customerId)
                .ExecuteUpdateAsync(c => c.SetProperty(cart => cart.IsActive, "N"));
        }

        private async Task UpdateProductVariantStocks(CreateOrderRequest request)
        {
            foreach (var detail in request.Details)
            {
                var variant = await _context.ProductVariants
                    .FirstOrDefaultAsync(v =>
                        v.Id == detail.ProductVariantId &&
                        v.ProductId == detail.ProductId
                    );

                if (variant != null)
                {
                    variant.StockQuantity -= detail.Quantity; // Assuming quantity is 1, modify if needed
                    _context.ProductVariants.Update(variant);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
