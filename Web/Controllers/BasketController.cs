using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Basket;
using Core.Interfaces;

namespace Web.Controllers
{
    [Route("api/v1/[controller]")]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetBasket()
        {
            return new [] { new Item { Id = 1, CatalogueItemId = 32, Quantity = 2, UnitPrice = 34.32m } };
        }

        [HttpPut]
        public ActionResult Index(Dictionary<string, int> items)
        {
            return NoContent();
        }

        [HttpPost]
        public ActionResult AddToBasket()
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult RemoveFromBasket(string id)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult Index(string id)
        {
            return Ok();
        }
    }
}
