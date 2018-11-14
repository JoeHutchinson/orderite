using System.Collections.Generic;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ApiModels;

namespace Web.Controllers
{
    [Route("api/v1/{memberId}/[controller]")]
    [Route("api/{memberId}/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _service;

        public BasketsController(IBasketService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBasket(string memberId, [FromBody]int basketId)
        {
            var basket = await _service.GetOrCreateBasket(memberId, basketId);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost("{basketId}")]
        public async Task<IActionResult> AddToBasket(int basketId, [FromBody]AddItem item)
        {
            await _service.AddItemToBasket(basketId, item.CatalogueItemId, 22m, item.Quantity);
            return Ok();
        }

        [HttpPatch("{basketId}")]
        public async Task<IActionResult> UpdateQuantities(string memberId, int basketId, [FromBody]Dictionary<string, int> items)
        {
            await _service.SetQuantities(basketId, items);
            return Ok();
        }

        [HttpGet("{basketId}")]
        public async Task<IActionResult> GetBasketById(string memberId, int basketId)
        {
            var basket = await _service.GetBasket(memberId, basketId);
            if (basket == null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpDelete("{basketId}")]
        public async Task<IActionResult> DeleteBasket(int basketId)
        {
            await _service.DeleteBasketAsync(basketId);
            return Ok();
        }
    }
}
