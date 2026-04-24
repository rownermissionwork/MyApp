using Account.Application.Common;
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
        Task<Result<string>> LoginAsync(UserLoginRequest request);
    }
}
