using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ApiModels;

namespace Web.Controllers
{
    [Route("api/v1/{memberId}/[controller]")]
    [Route("api/{memberId}/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ICatalogueService _catalogueService;

        public BasketsController(IBasketService basketService, ICatalogueService catalogueService)
        {
            _basketService = basketService;
            _catalogueService = catalogueService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(string memberId, [FromBody]int basketId)
        {
            var basket = await _basketService.GetOrCreateBasket(memberId, basketId);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost("{basketId}")]
        public async Task<IActionResult> AddToBasket(int basketId, [FromBody]AddItem item)
        {
            var catalogueItem = await _catalogueService.GetCatalogueItem(item.CatalogueItemId);
            if (catalogueItem == null)
            {
                return NotFound();
            }

            await _basketService.AddItemToBasket(basketId, catalogueItem.Id, catalogueItem.Price, item.Quantity);
            return Ok();
        }

        [HttpPatch("{basketId}")]
        public async Task<IActionResult> UpdateQuantities(string memberId, int basketId, [FromBody]Dictionary<string, int> items)
        {
            await _basketService.SetQuantities(basketId, items);
            return Ok();
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasketById(string memberId, int basketId)
        {
            var basket = await _basketService.GetBasket(memberId, basketId);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpDelete("{basketId}")]
        public async Task<IActionResult> DeleteBasket(int basketId)
        {
            await _basketService.DeleteBasketAsync(basketId);
            return Ok();
        }
    }
}
