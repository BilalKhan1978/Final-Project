using JustGoApi.Models;
using JustGoApi.ViewModels;

namespace JustGoApi.Services
{
    public interface IJustGoService
    {
        Task<List<Contact>> GetAllContacts();
        Task<Contact> GetContact(Guid id);
        Task<Contact> AddContact(AddContactRequest addContactRequest);
        Task<Contact> UpdateContact(Guid id, UpdateContactRequest updateContactRequest);
        Task<Contact> DeleteContact(Guid id);
    }
}
