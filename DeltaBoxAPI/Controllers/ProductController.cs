﻿using DeltaboxAPI.Application.Common.Pagings;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Commands;
using DeltaboxAPI.Application.Requests.DeltaBoxAPI.Product.Queries;
using DeltaboxAPI.Domain.Entities.DeltaBox.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DeltaboxAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdateProductCategory(ProductCategory command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateProductCategory(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductCategory(int? id, string? name, string? isPopular, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetProductCategory(id, name, isPopular, isActive, getAll, currentPage, itemsPerPage));

                PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateOrUpdateProduct(CreateOrUpdateProductRequest command)
        {
            try
            {
                var result = await _mediator.Send(new CreateOrUpdateProduct(command));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetProduct(int? id, int? categoryId, string? name, string? isActive, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetProduct(id, categoryId, name, isActive, getAll, currentPage, itemsPerPage));

                PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterProducts(string? keyword, int? id, string? categoryId, string? latestOffer, string? stock,decimal? minPrice, decimal? maxPrice, string? plan, string? duration, string? sortBy, string? getAll, int currentPage, int itemsPerPage)
        {
            try
            {
                var result = await _mediator.Send(new GetFilterProducts(keyword, id, categoryId, latestOffer, stock, minPrice, maxPrice, plan, duration, sortBy, getAll, currentPage, itemsPerPage));

                PaginationHeader.Add(Response, result.CurrentPage, result.ItemsPerPage, result.TotalPages, result.TotalItems);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilterOptions()
        {
            try
            {
                var result = await _mediator.Send(new GetFilterOptions());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductDetails(int? id)
        {
            try
            {
                var result = await _mediator.Send(new GetProductDetails(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
