using System.ComponentModel.DataAnnotations;

namespace JustGoApi.ViewModels
{
    public class AddListingRequest
    {
        [Required]
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
