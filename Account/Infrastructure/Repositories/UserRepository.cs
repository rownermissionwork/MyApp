using Account.Domain.Entities;
using Account.Domain.Interfaces;
using Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories
{
    public class UserRepository(AuthDbContext context) : IUserRepository
    {
        private readonly AuthDbContext _authDbContext = context;

        public async Task<UserLogin?> GetUserByUserNameAsync(string userName)
        {
            try {
                return await _authDbContext.UserLogin.FirstOrDefaultAsync(u => u.UserName == userName);
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
