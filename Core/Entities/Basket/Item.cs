namespace Core.Entities.Basket
{
    public class Item : Entity
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int CatalogueItemId { get; set; }
    }
}
