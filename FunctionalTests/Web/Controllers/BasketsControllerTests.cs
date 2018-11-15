using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Web;
using Web.ApiModels;

namespace FunctionalTests.Web.Controllers
{
    [TestClass]
    public class BasketsControllerTests
    {
        [TestMethod]
        public async Task CanCreateBasket()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(new CreateBasket() { BasketId = 1 }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("/api/joe/baskets", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
