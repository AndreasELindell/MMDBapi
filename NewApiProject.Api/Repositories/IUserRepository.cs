using Microsoft.AspNetCore.Mvc;
using NewApiProject.Api.Entites;

namespace NewApiProject.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUser(User user);
        Task<User?> GetUserByUsername(string username);
        Task RegisterUser(User user);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> CheckJwtRefreshToken(string refreshToken);
        Task<bool> SaveChanges();
        Task<string> GetUserNameById(int id);
    }
}
