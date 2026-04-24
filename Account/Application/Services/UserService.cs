using Account.Application.Common;
using Account.Application.Dtos.User;
using Account.Application.Interfaces;
using Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Services
{
    public class UserService(IUserRepository userRepository, IUtilityService utilityRepository,IJwtService  jwtService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUtilityService _utilityRepository = utilityRepository;
        private readonly IJwtService _jwtService = jwtService;

        public async Task<Result<string>> LoginAsync(UserLoginRequest request)
        {
            try
            {
                
                var result = await _userRepository.GetUserByUserNameAsync(request.UserName);
                if (result is null)
                {
                    return Result<string>.Failure("user not found");
                }
                else if (!_utilityRepository.VerifyHashed(request.UserName, request.Password, result.PasswordHash))
                {
                    return Result<string>.Failure("Invalid password");
                }
                else if (!result.IsActive)
                {
                    return Result<string>.Failure("user is not active");
                }
                else if (result.IsLocked) {
                    return Result<string>.Failure("user account is locked");
                }
                var details = new {
                    userId = result.UserID,
                    userName = result.UserName,
                    Role = "Admin"
                };
                
                var token = _jwtService.GenerateToken(details);

                return Result<string>.Success(token);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
