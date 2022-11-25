using JustGoApi.Models;

namespace JustGoApi.ViewModels
{
    public class ListingDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
