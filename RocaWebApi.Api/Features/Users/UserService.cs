using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RocaWebApi.Api.Features.Users
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user);
        Task<User> Update(int userId, User user);
        Task Delete(int id);
    }

    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _dbContext.Users.ToListAsync();
            return users;
        }

        public Task<User> GetById(int id)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Create(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User> Update(int id, User user)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (userEntity == null)
            {
                return null;
            }

            // _mapper.Map(user, userEntity);

            await _dbContext.SaveChangesAsync();

            return userEntity;
        }

        public async Task Delete(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return;
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}
