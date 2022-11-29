using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JustGoApi.Models
{
    public class Listing
    {
      public int Id { get; set; }
      public string Title { get; set; }
      [NotMapped]
      public byte[] Image { get; set; }
      public string? ImageName { get; set; }
      public decimal Price { get; set; }
      [Required]
      public Category Category { get; set; }
      [Required]
      public User User { get; set; }
    }
}
