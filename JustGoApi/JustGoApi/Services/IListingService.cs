using JustGoApi.Models;
using JustGoApi.ViewModels;

namespace JustGoApi.Services
{
    public interface IListingService
    {
        Task<List<ListingDto>> GetAllListing();
        Task<ListingDto> GetOnelisting(int id);
        Task AddListing(AddListingRequest addContactRequest);
        Task UpdateListing(int id, UpdateListingRequest updateContactRequest);
        Task<byte[]> GetImage(int id);
    }
}
