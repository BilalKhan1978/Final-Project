using JustGoApi.Data;
using JustGoApi.Models;
using JustGoApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JustGoApi.Services
{
    public class ListingService : IListingService
    {
        private readonly ContactsDbContext _dbContext;
        public ListingService(ContactsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ListingDto>> GetAllListing()
        {
            var listings = await _dbContext.Listings.Include(x => x.Category).Include(x => x.User).ToListAsync(); 
            return listings.Select(x => x.ToDto()).ToList();
        }
        public async Task<ListingDto> GetOnelisting(int id)
        {
            var listing = await _dbContext.Listings.Include(x=>x.Category).Include(x=>x.User).FirstOrDefaultAsync(x=>x.Id==id);
            if (listing == null)
                throw new Exception("Not found");
            return listing.ToDto();
        }
        public async Task AddListing(AddListingRequest addListingRequest)
        {
            var category = await _dbContext.Categories.FindAsync(addListingRequest.CategoryId);
            var user = await _dbContext.Users.FindAsync(addListingRequest.UserId);
            var bytes = await addListingRequest.Image.GetBytes();
            var listing = new Listing()
            {
                Title=addListingRequest.Title,
                Price=addListingRequest.Price,
                Image= bytes,
                Category=category,
                User = user
            };
            await _dbContext.Listings.AddAsync(listing);
            await _dbContext.SaveChangesAsync();
            
        }
        public async Task UpdateListing(int id, UpdateListingRequest updateListingRequest)
        {
            var listing = await _dbContext.Listings.FindAsync(id);
            

            if (listing == null)
            {
                throw new Exception("Not found");

            }
            var category = await _dbContext.Categories.FindAsync(updateListingRequest.CategoryId);
            var bytes = await updateListingRequest.Image.GetBytes();
            listing.Title = updateListingRequest.Title;
            listing.Price = updateListingRequest.Price;
            listing.Image = bytes;
            listing.Category = category;

            await _dbContext.SaveChangesAsync();

        }
        public async Task<byte[]> GetImage(int id)
        {
            var listing = await _dbContext.Listings.FindAsync(id);


            if (listing == null)
            {
                throw new Exception("Not found");

            }
            return listing.Image;

        }
    }
}
