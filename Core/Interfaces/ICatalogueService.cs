using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICatalogueService
    {
        Task<CatalogItem> GetCatalogueItem(int catalogueItemId);
    }
}
