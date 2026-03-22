using Account.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> LoginAsync(UserLoginRequest request);
    }
}
