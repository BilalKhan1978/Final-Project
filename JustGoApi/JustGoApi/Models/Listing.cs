using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;

namespace JustGoApi.Models
{
    public class Listing
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public byte[] Image { get; set; }
      public float Price { get; set; }
      public Category CategoryId { get; set; }
      public User UserId { get; set; }
    }
}
