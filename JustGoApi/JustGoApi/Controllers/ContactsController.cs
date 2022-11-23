using JustGoApi.Services;
using JustGoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace ContactsApiProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly IJustGoService _justGoService;
        public ContactsController(IJustGoService justGoService)
        {
            _justGoService = justGoService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            return Ok(await _justGoService.GetAllContacts());   
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = await _justGoService.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }


        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            return Ok(await _justGoService.AddContact(addContactRequest)); 
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid
                id, UpdateContactRequest updateContactRequest)
        {
            var contact = await _justGoService.UpdateContact(id,updateContactRequest);

            if (contact==null)
            {
                return NotFound();
            }
            return Ok(contact);

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await _justGoService.DeleteContact(id);
            if (contact==null)
            {
                return NotFound();
            }
            return Ok(contact);
        }
    }
}
