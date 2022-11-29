using JustGoApi.Models;
using Microsoft.AspNetCore.Http;

namespace JustGoApi.ViewModels
{
    public static class DtoExtension
    {
        public static ListingDto ToDto(this Listing listingModel)
        {
            return new ListingDto
            {
                Id = listingModel.Id,
                Title = listingModel.Title,
                Price = listingModel.Price,
                CategoryId = listingModel.Category.Id,
                ImageName=listingModel.ImageName,
                UserId  =   listingModel.User.Id

            };
        }

        public static async Task<byte[]> GetBytes(this IFormFile formFile)
        {
            await using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
