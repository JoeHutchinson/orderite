using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Basket;
using Core.Interfaces;

namespace Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;

        public BasketService(IAsyncRepository<Basket> basketRepository)
        {
            this._basketRepository = basketRepository;
        }

        public async Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            basket.AddItem(catalogItemId, price, quantity);
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task DeleteBasketAsync(int basketId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            await _basketRepository.DeleteAsync(basket);
        }

        public async Task<Basket> GetOrCreateBasket(string buyerId, int basketId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            if (basket != null)
            {
                return basket;
            }

            basket = new Basket { BuyerId = buyerId, Id = basketId };
            return await _basketRepository.AddAsync(basket);
        }

        public async Task RemoveItemFromBasket(int basketId, int catalogueItemId)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            basket.RemoveItem(catalogueItemId);
            await _basketRepository.UpdateAsync(basket);
        }

        public async Task SetQuantities(int basketId, Dictionary<string, int> items)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            foreach (var item in basket.Items)
            {
                if (items.TryGetValue(item.CatalogueItemId.ToString(), out var quantity))
                {
                    item.Quantity = quantity;
                }
            }

            await _basketRepository.UpdateAsync(basket);
        }
    }
}
