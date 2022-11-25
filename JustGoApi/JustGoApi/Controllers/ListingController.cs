using JustGoApi.Models;
using JustGoApi.Services;
using JustGoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JustGoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;
        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllListing()
        {
            return Ok(await _listingService.GetAllListing());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOnelisting([FromRoute] int id)
        {
            
                var listing = await _listingService.GetOnelisting(id);
               
                if (listing == null)
                {
                    return NotFound();
                }
                return Ok(listing);


            }


        [HttpPost]
        public async Task<IActionResult> AddContact([FromForm]AddListingRequest addListingRequest)
        {
            await _listingService.AddListing(addListingRequest);
            return Ok();
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateContact([FromRoute] int
                id, [FromForm] UpdateListingRequest updateListingRequest)
        {

            try
            {
                await _listingService.UpdateListing(id, updateListingRequest);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("Not found"))
                {
                    return NotFound();
                }
                throw new Exception(e.Message);

            }          

        }

        [HttpGet("{id}/image")]
        public async Task<IActionResult> GetImage([FromRoute] int id)
        {
            var image = await _listingService.GetImage(id).ConfigureAwait(false);
            return File(image, "image/png");
        }
    }
}
