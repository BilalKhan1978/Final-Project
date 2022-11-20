using JustGoApi.Data;
using JustGoApi.Models;
using JustGoApi.Services;
using JustGoApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            //var contact = new Contact()
            //{
            //    Id = Guid.NewGuid(),
            //    FullName = addContactRequest.FullName,
            //    Email = addContactRequest.Email,
            //    Phone = addContactRequest.Phone,
            //    Address = addContactRequest.Address
            //};

            //await _justGoService.AddContact(addContactRequest);
            //await _justGoService.SaveChangesAsync();

            return Ok(await _justGoService.AddContact(addContactRequest)); 

            //return Ok(contact);
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

            //if (contact != null)
            //{
            //    contact.FullName = updateContactRequest.FullName;
            //    contact.Email = updateContactRequest.Email;
            //    contact.Phone = updateContactRequest.Phone;
            //    contact.Address = updateContactRequest.Address;

            //    await _dbContext.SaveChangesAsync();
            //    return Ok(contact);
            //}
            //return NotFound();
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

            //var contact = await _dbContext.Contacts.FindAsync(id);

            //if (contact != null)
            //{
            //    _dbContext.Remove(contact);
            //    await _dbContext.SaveChangesAsync();
            //    return Ok(contact);
            //}

            //return NotFound();
        }


    }
}
