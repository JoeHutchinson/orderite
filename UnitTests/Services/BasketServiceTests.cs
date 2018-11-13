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
        private Basket _basket = new Basket
        {
            BuyerId = Root.Any.String(),
            Id = Root.Any.Integer()
        };

        [TestMethod]
        public async Task ItemAddedToEmptyBasket()
        {
            var mockRepository = CreateMockBasketRepositoryForBasket(_basket);
            var sut = new BasketService(mockRepository.Object);

            await sut.AddItemToBasket(_basket.Id, Root.Any.Integer(), Root.Any.Decimal(), Root.Any.Integer());

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == _basket)), Times.Once);
        }

        [TestMethod]
        public async Task BasketCanBeDeleted()
        {
            var mockRepository = CreateMockBasketRepositoryForBasket(_basket);
            var sut = new BasketService(mockRepository.Object);

            await sut.DeleteBasketAsync(_basket.Id);

            mockRepository.Verify(x => x.DeleteAsync(It.Is<Basket>(i => i == _basket)), Times.Once);
        }

        [TestMethod]
        public async Task QuantityNotUpdatedForUnknownItem()
        {
            var mockRepository = CreateMockBasketRepositoryForBasket(_basket);
            var sut = new BasketService(mockRepository.Object);

            await sut.SetQuantities(_basket.Id, new Dictionary<string, int> { { Root.Any.String(), Root.Any.Integer() } });

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == _basket)), Times.Once);
            var item = _basket.Items.FirstOrDefault();
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
            var sut = new BasketService(mockRepository.Object);

            var newQuantity = 2;
            await sut.SetQuantities(basket.Id, new Dictionary<string, int> { {catalogueItemId.ToString(), newQuantity} });

            mockRepository.Verify(x => x.UpdateAsync(It.Is<Basket>(i => i == basket)), Times.Once);
            var item = basket.Items.FirstOrDefault();
            Assert.AreEqual(newQuantity, item.Quantity);
        }

        private static Mock<IAsyncRepository<Basket>> CreateMockBasketRepositoryForBasket(Basket basket)
        {
            var mockRepository = new Mock<IAsyncRepository<Basket>>();
            mockRepository.Setup(x => x.GetByIdAsync(It.Is<int>(i => i == basket.Id))).Returns(Task.Run(() => basket));
            mockRepository.Setup(x => x.UpdateAsync(It.Is<Basket>(i => i == basket))).Returns(Task.Run(() => basket));
            return mockRepository;
        }
    }
}
