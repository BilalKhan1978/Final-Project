using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System.ComponentModel.DataAnnotations;

namespace JustGoApi.Models
{
    public class Listing
    {
      public int Id { get; set; }
      public string Title { get; set; }
      public byte[] Image { get; set; }
      public float Price { get; set; }
      [Required]
      public Category Category { get; set; }
      [Required]
      public User User { get; set; }
    }
}
