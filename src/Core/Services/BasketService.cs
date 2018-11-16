using Core.Entities.Basket;
using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
    public class BasketService : IBasketService
    {
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IAsyncRepository<Basket> basketRepository,
            ILogger<BasketService> logger)
        {
            _basketRepository = basketRepository;
            _logger = logger;
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
                _logger.Info($"No basket found for {buyerId}");
                return basket;
            }

            basket = new Basket { BuyerId = buyerId, Id = basketId };
            return await _basketRepository.AddAsync(basket);
        }

        public async Task RemoveItemsFromBasket(int basketId, List<int> catalogueItemIds)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            foreach (var catalogueItemId in catalogueItemIds)
            {
                _logger.Info($"Removing item {catalogueItemId} from basket {basketId}");
                basket.RemoveItem(catalogueItemId);
            }

            await _basketRepository.UpdateAsync(basket);
        }

        public async Task<Basket> GetBasket(string buyerId, int basketId)
        {
            return await _basketRepository.GetByIdAsync(basketId);
        }

        public async Task SetQuantities(int basketId, Dictionary<string, int> items)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);

            foreach (var item in basket.Items)
            {
                if (items.TryGetValue(item.CatalogueItemId.ToString(), out var quantity))
                {
                    _logger.Info($"Updating quantity of item {item.Id} to {quantity}");
                    item.Quantity = quantity;
                }
            }

            await _basketRepository.UpdateAsync(basket);
        }
    }
}
