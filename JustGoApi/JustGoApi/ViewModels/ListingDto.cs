using JustGoApi.Models;

namespace JustGoApi.ViewModels
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string? ImageSource { get; set; }
        public string? ImageName { get; set; }
    }
}
