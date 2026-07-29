using Account.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<UserLogin?> GetUserByUserNameAsync(string userName);
        Task RegisterAsync(Domain.Entities.UserProfile request, CancellationToken cancellationToken = default);
        Task<List<Domain.Entities.UserRole>> GetRole(CancellationToken cancellationToken = default);
    }
}
