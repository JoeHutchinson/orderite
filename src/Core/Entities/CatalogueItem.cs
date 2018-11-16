namespace Core.Entities
{
    public class CatalogItem : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
