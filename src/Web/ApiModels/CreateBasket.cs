using System.ComponentModel.DataAnnotations;

namespace Web.ApiModels
{
    public class CreateBasket
    {
        [Required]
        public int? BasketId { get; set; }
    }
}