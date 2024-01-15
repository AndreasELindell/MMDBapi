using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewApiProject.Api.DbContext;
using NewApiProject.Api.Entites;

namespace NewApiProject.Api.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MovieContext _context;

        public UserRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUser(User user)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
        }
        public async Task<string> GetUserNameById(int id) 
        { 
            var user = await _context.Users.FirstOrDefaultAsync(u =>u.Id == id);

            if(user == null) 
            {
                return "Anonymous user";
            }

            return user.Username;
        }
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CheckJwtRefreshToken(string refreshToken)
        {
            return await _context.Users.AnyAsync(u => u.RefreshToken == refreshToken);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}
