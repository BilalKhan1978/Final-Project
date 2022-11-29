using JustGoApi.Models;
using JustGoApi.Services;
using JustGoApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace JustGoApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ListingController : Controller
    {
        private readonly IListingService _listingService;

        private readonly IWebHostEnvironment _hostEnvironment;

        public ListingController(IListingService listingService, IWebHostEnvironment hostEnvironment)
        {
            _listingService = listingService;
            _hostEnvironment = hostEnvironment;
        }
        
        // I edited here
        [HttpGet]
        public async Task<IActionResult> GetAllListing()
        {
            var listingList = await _listingService.GetAllListing();
            listingList.ForEach(x => x.ImageSource = x.ImageName == null ? null :
             String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName));
            return Ok(listingList);
        }

        // I edited here
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOnelisting([FromRoute] int id)
        {
            
                var listing = await _listingService.GetOnelisting(id);
               
                if (listing == null)
                {
                    return NotFound();
                }
                listing.ImageSource = listing.ImageName==null? null: String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, listing.ImageName);
                return Ok(listing);


            }

        // I edited here
        [HttpPost]
        public async Task<IActionResult> AddListing([FromForm]AddListingRequest addListingRequest)
        {
            try
            {
                var fileName = addListingRequest.Image==null?null: await SaveImage(addListingRequest.Image);
                await _listingService.AddListing(addListingRequest, fileName);
                return Ok();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateListing([FromRoute] int
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

        //[HttpGet("{id}/image")]
        //public async Task<IActionResult> GetImage([FromRoute] int id)
        //{
        //    var image = await _listingService.GetImage(id).ConfigureAwait(false);
        //    return File(image, "image/png");
        //}
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName= new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ','-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff")+ Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

    }
}
