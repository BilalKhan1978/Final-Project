using JustGoApi.Data;
using JustGoApi.Models;
using JustGoApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace JustGoApi.Services
{
    public class JustGoService : IJustGoService
    {
        private readonly ContactsDbContext _dbContext;
        public JustGoService(ContactsDbContext dbContext)
        {
        _dbContext= dbContext;
        }

        public async Task<List<Contact>> GetAllContacts()
        {
          return  await _dbContext.Contacts.ToListAsync();
        }
        public async Task<Contact> GetContact(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            return contact;
        }
        public async Task<Contact> AddContact(AddContactRequest addContactRequest) 
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                FullName = addContactRequest.FullName,
                Email = addContactRequest.Email,
                Phone = addContactRequest.Phone,
                Address = addContactRequest.Address
            };
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return contact;
        }
        public async Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest) 
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Email = updateContactRequest.Email;
                contact.Phone = updateContactRequest.Phone;
                contact.Address = updateContactRequest.Address;

                await _dbContext.SaveChangesAsync();
                return contact;
            }
            return contact;
        }
        public async Task<Contact> DeleteContact(Guid id) 
        {
            var contact = await _dbContext.Contacts.FindAsync(id);

            if (contact != null)
             {
                _dbContext.Remove(contact);
                await _dbContext.SaveChangesAsync();
                return (contact);
             }
            return contact;

        }
    }
}
