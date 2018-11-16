using System.Collections.Generic;
using System.Linq;

namespace Core.Entities.Basket
{
    public class Basket : Entity
    {
        public string BuyerId { get; set; }
        private readonly List<Item> _items = new List<Item>();
        public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

        public void AddItem(int catalogueItemId, decimal unitPrice, int quantity = 1)
        {
            if (Items.All(i => i.CatalogueItemId != catalogueItemId))
            {
                _items.Add(
                    new Item
                    {
                        CatalogueItemId = catalogueItemId,
                        Quantity = quantity,
                        UnitPrice = unitPrice
                    }
                );
                return;
            }

            var existingItem = Items.FirstOrDefault(i => i.CatalogueItemId == catalogueItemId);
            existingItem.Quantity += quantity;
        }

        public void RemoveItem(int catalogueItemId)
        {
            var existingItem = Items.FirstOrDefault(i => i.CatalogueItemId == catalogueItemId);
            if (existingItem == null)
            {
                return;
            }

            _items.Remove(existingItem);
        }
    }
}
