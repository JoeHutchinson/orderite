using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Basket;

namespace Core.Interfaces
{
    public interface IBasketService
    {
        Task AddItemToBasket(int basketId, int catalogItemId, decimal price, int quantity);
        Task SetQuantities(int basketId, Dictionary<string, int> quantities);
        Task DeleteBasketAsync(int basketId);
        Task<Basket> GetOrCreateBasket(string buyerId, int basketId);
        Task RemoveItemsFromBasket(int basketId, List<int> catalogueItemIds);
        Task<Basket> GetBasket(string buyerId, int basketId);
    }
}
