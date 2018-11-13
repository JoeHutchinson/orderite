using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class BasketController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<string>> Index()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> items)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(string id)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Index(string id)
        {
            return Ok();
        }
    }
}
