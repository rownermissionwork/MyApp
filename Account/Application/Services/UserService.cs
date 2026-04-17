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

        public async Task<string> LoginAsync(UserLoginRequest request)
        {
            try
            {
                
                var result = await _userRepository.GetUserByUserNameAsync(request.UserName);
                if (result is null)
                {
                    return "User not found";
                }
                else if (!_utilityRepository.VerifyHashed(request.UserName,request.Password,result.PasswordHash))
                {
                    return "Invalid password";
                }
                var details = new {
                    userId = result.UserID,
                    userName = result.UserName,
                    Role = "Admin"
                };
                
                var token = _jwtService.GenerateToken(details);

                return "OK";
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
