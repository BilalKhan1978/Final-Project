using JustGoApi.Models;
using JustGoApi.ViewModels;

namespace JustGoApi.Services
{
    public interface IUserService
    {
        Task<User> GetOneUser(int id);
        Task AddUser(AddUserRequest addUserRequest);
        Task UpdateUser(int id, UpdateUserRequest updateUserRequest);
    }
}
