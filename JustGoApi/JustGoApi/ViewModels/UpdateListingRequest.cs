using System.ComponentModel.DataAnnotations;

namespace JustGoApi.ViewModels
{
    public class UpdateListingRequest
    {
        [Required]
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        public float Price { get; set; }
        [Required]
        public int CategoryId { get; set; }        
    }
}
