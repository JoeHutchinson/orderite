﻿using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Basket;
using NSwag.Annotations;
using Web.ApiModels;

namespace Web.Controllers
{
    [Route("api/v1/{memberId}/[controller]")]
    [Route("api/{memberId}/[controller]")]
    public class BasketsController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogueService _catalogueService;

        public BasketsController(IBasketService basketService, ICatalogueService catalogueService)
        {
            _basketService = basketService;
            _catalogueService = catalogueService;
        }

        [HttpPost]
        [Description("Create basket")]
        [SwaggerResponse(typeof(Basket))]
        public async Task<IActionResult> CreateBasket(string memberId, [FromBody]CreateBasket createBasket)
        {
            var basket = await _basketService.GetOrCreateBasket(memberId, createBasket.BasketId.Value);
            if (basket == null)
            {
                return NotFound("No Basket found");
            }

            return Ok(basket);
        }

        [HttpPost("{basketId}")]
        [Description("Add item to basket")]
        [SwaggerResponse(typeof(void))]
        public async Task<IActionResult> AddToBasket(int basketId, [FromBody]AddItem item)
        {
            var catalogueItem = await _catalogueService.GetCatalogueItem(item.CatalogueItemId.Value);
            if (catalogueItem == null)
            {
                return NotFound("No CatalogueItem found");
            }

            await _basketService.AddItemToBasket(basketId, catalogueItem.Id, catalogueItem.Price, item.Quantity.Value);
            return Ok();
        }

        [HttpPatch("{basketId}")]
        [Description("Update quantities of items")]
        [SwaggerResponse(typeof(void))]
        public async Task<IActionResult> UpdateQuantities(string memberId, int basketId, [FromBody]Dictionary<string, int> items)
        {
            await _basketService.SetQuantities(basketId, items);
            return Ok();
        }

        [HttpGet("{basketId}")]
        [Description("Get Basket")]
        [SwaggerResponse(typeof(Basket))]
        public async Task<IActionResult> GetBasketById(string memberId, int basketId)
        {
            var basket = await _basketService.GetBasket(memberId, basketId);
            if (basket == null)
            {
                return NotFound("No Basket found");
            }

            return Ok(basket);
        }

        [HttpDelete("{basketId}")]
        [Description("Delete items or basket")]
        [SwaggerResponse(typeof(void))]
        public async Task<IActionResult> DeleteBasket(int basketId, [FromBody]List<int> catalogueItemIds)
        {
            if (catalogueItemIds.Any())
            {
                await _basketService.RemoveItemsFromBasket(basketId, catalogueItemIds);
            }
            else
            {
                await _basketService.DeleteBasketAsync(basketId);
            }

            return Ok();
        }
    }
}
