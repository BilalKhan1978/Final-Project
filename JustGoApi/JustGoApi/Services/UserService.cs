using JustGoApi.Data;
using JustGoApi.Models;
using JustGoApi.ViewModels;
using System.Reflection;

namespace JustGoApi.Services
{
    public class UserService : IUserService 
    {
        private readonly ReuseDbContext _dbContext;

        public UserService(ReuseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetOneUser(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            return user;
        }
        public async Task AddUser(AddUserRequest addUserRequest)
        {
            var user = new User()
            {
                Name = addUserRequest.Name,
                Email = addUserRequest.Email,
                Password = addUserRequest.Password
            };
           await _dbContext.Users.AddAsync(user);  
           await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateUser(int id, UpdateUserRequest updateUserRequest)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Not found");
            }
            user.Name = updateUserRequest.Name;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password;
            await _dbContext.SaveChangesAsync();
        }
    }
}
