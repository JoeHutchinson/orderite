using System.ComponentModel.DataAnnotations;

namespace Web.ApiModels
{
    public class AddItem
    {
        [Required]
        public int? CatalogueItemId { get; set; }

        [Required]
        public int? Quantity { get; set; }
    }
}