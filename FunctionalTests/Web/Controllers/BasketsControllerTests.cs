using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web;
using Web.ApiModels;

namespace FunctionalTests.Web.Controllers
{
    [TestClass]
    public class BasketsControllerTests
    {
        [TestMethod]
        public async Task CreateBasket()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(new CreateBasket() { BasketId = 1 }));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync("/api/joe/baskets", content);
            response.EnsureSuccessStatusCode();
        }

        [TestMethod]
        public async Task GetBasketUnknownBasket()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();
            var response = await client.GetAsync("/api/joe/baskets/1");
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
