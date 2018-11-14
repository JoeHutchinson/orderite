using System.Collections.Generic;
using System.Linq;
using Core.Entities.Basket;
using Core.Interfaces;
using Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Numbers;
using TddXt.AnyRoot.Strings;

namespace UnitTests.Services
{
    [TestClass]
    public class BasketServiceTests
    {
        [TestMethod]
        public async Task ItemAddedToEmptyBasket()
        {
            var basket = CreateEmptyBasket();
            var mockRepository = CreateMockBasketRepositoryForBasket(basket);
            var sut = new BasketService(mockRepository.Object, CreateMockLogger());

            await sut.AddItemToBasket(basket.Id, Root.Any.Integer(), Root.Any.Decimal(), Root.Any.Integer());

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == basket)), Times.Once);
        }

        [TestMethod]
        public async Task BasketCanBeDeleted()
        {
            var basket = CreateEmptyBasket();
            var mockRepository = CreateMockBasketRepositoryForBasket(basket);
            var sut = new BasketService(mockRepository.Object, CreateMockLogger());

            await sut.DeleteBasketAsync(basket.Id);

            mockRepository.Verify(x => x.DeleteAsync(It.Is<Basket>(i => i == basket)), Times.Once);
        }

        [TestMethod]
        public async Task QuantityNotUpdatedForUnknownItem()
        {
            var basket = CreateEmptyBasket();
            var mockRepository = CreateMockBasketRepositoryForBasket(basket);
            var sut = new BasketService(mockRepository.Object, CreateMockLogger());

            await sut.SetQuantities(basket.Id, new Dictionary<string, int> { { Root.Any.String(), Root.Any.Integer() } });

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == basket)), Times.Once);
            var item = basket.Items.FirstOrDefault();
            Assert.IsNull(item);
        }

        [TestMethod]
        public async Task QuantityOfKnownItemIsUpdated()
        {
            var basket = new Basket
            {
                BuyerId = Root.Any.String(),
                Id = Root.Any.Integer()
            };
            var catalogueItemId = Root.Any.Integer();
            var unitPrice = Root.Any.Decimal();
            var quantity = Root.Any.Integer();
            basket.AddItem(catalogueItemId, unitPrice, quantity);

            var mockRepository = CreateMockBasketRepositoryForBasket(basket);
            var sut = new BasketService(mockRepository.Object, CreateMockLogger());

            const int newQuantity = 2;
            await sut.SetQuantities(basket.Id, new Dictionary<string, int> { {catalogueItemId.ToString(), newQuantity} });

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == basket)), Times.Once);
            var item = basket.Items.FirstOrDefault();
            Assert.AreEqual(newQuantity, item.Quantity);
        }

        [TestMethod]
        public async Task ItemRemovedFromBasket()
        {
            var basket = CreateEmptyBasket();
            var mockRepository = CreateMockBasketRepositoryForBasket(basket);
            var sut = new BasketService(mockRepository.Object, CreateMockLogger());
            await sut.AddItemToBasket(basket.Id, Root.Any.Integer(), Root.Any.Decimal(), Root.Any.Integer());

            await sut.RemoveItemFromBasket(basket.Id, basket.Items.FirstOrDefault().CatalogueItemId);

            Assert.AreEqual(0, basket.Items.Count);
            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == basket)), Times.Exactly(2));
        }

        private static Basket CreateEmptyBasket()
        {
            return new Basket
            {
                BuyerId = Root.Any.String(),
                Id = Root.Any.Integer()
            };
        }

        private static Mock<IAsyncRepository<Basket>> CreateMockBasketRepositoryForBasket(Basket basket)
        {
            var mockRepository = new Mock<IAsyncRepository<Basket>>();
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(i => i == basket.Id))).Returns(Task.Run(() => basket));
            mockRepository.Setup(x => x.UpdateAsync(It.Is<Basket>(i => i == basket))).Returns(Task.Run(() => basket));
            return mockRepository;
        }

        private static ILogger<BasketService> CreateMockLogger()
        {
            return new Mock<ILogger<BasketService>>().Object;
        }
    }
}
