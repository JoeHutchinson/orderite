using System.Linq;
using Core.Entities.Basket;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Numbers;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace UnitTests.Core.BasketTests
{
    [TestClass]
    public class BasketTests
    {
        private readonly int _catalogueItemId = Root.Any.Integer();
        private readonly decimal _unitPrice = Root.Any.Decimal();
        private readonly int _quantity = Root.Any.Integer();

        [TestMethod]
        public void ItemNotPresentInBasketItCanBeAdded()
        {
            var sut = new Basket();

            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            var item = sut.Items.FirstOrDefault();
            Assert.IsNotNull(item);
            Assert.AreEqual(_catalogueItemId, item.CatalogueItemId);
            Assert.AreEqual(_unitPrice, item.UnitPrice);
            Assert.AreEqual(_quantity, item.Quantity);
        }

        [TestMethod]
        public void ItemNotPresentInBasketItCanBeAddedQuantityDefaultsToOne()
        {
            var sut = new Basket();

            sut.AddItem(_catalogueItemId, _unitPrice);

            var item = sut.Items.FirstOrDefault();
            Assert.IsNotNull(item);
            Assert.AreEqual(_catalogueItemId, item.CatalogueItemId);
            Assert.AreEqual(_unitPrice, item.UnitPrice);
            Assert.AreEqual(1, item.Quantity);
        }

        [TestMethod]
        public void ItemPresentInBasketQuantityIsUpdated()
        {
            var sut = new Basket();
            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            var item = sut.Items.FirstOrDefault();
            Assert.AreEqual(_quantity + _quantity, item.Quantity);
        }

        [TestMethod]
        public void ItemPresentInBasketUnitPriceUnchanged()
        {
            var sut = new Basket();
            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            var item = sut.Items.FirstOrDefault();
            Assert.AreEqual(_unitPrice, item.UnitPrice);
        }

        [TestMethod]
        public void ItemPresentInBasketCanBeRemoved()
        {
            var sut = new Basket();
            sut.AddItem(_catalogueItemId, _unitPrice, _quantity);

            sut.RemoveItem(_catalogueItemId);

            var item = sut.Items.FirstOrDefault();
            Assert.IsNull(item);
        }
    }
}
