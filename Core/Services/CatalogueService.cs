using Core.Entities;
using Core.Interfaces;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CatalogueService : ICatalogueService
    {
        private readonly IAsyncRepository<CatalogItem> _catalogueItemRepository;
        private readonly ILogger<CatalogueService> _logger;

        public CatalogueService(IAsyncRepository<CatalogItem> catalogueItemRepository,
            ILogger<CatalogueService> logger)
        {
            _catalogueItemRepository = catalogueItemRepository;
            _logger = logger;

            _catalogueItemRepository.AddAsync(
                new CatalogItem
                {
                    Id = 1,
                    Name = "biscuits",
                    Description = "cruncy biscuits",
                    Price = 1.20m
                });
            _catalogueItemRepository.AddAsync(
                new CatalogItem
                {
                    Id = 2,
                    Name = "cake",
                    Description = "old cake",
                    Price = 5.35m
                });
            catalogueItemRepository.AddAsync(
                new CatalogItem
                {
                    Id = 3,
                    Name = "bread",
                    Description = "fresh bread",
                    Price = 0.80m
                });
        }

        public async Task<CatalogItem> GetCatalogueItem(int catalogueItemId)
        {
            return await _catalogueItemRepository.GetByIdAsync(catalogueItemId);
        }
    }
}
