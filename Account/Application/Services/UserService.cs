using Account.Application.Common;
using Account.Application.Dtos.User;
using Account.Application.Interfaces;
using Account.Domain.Entities;
using Account.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Account.Application.Services
{
    public class UserService(IUserRepository userRepository, IUtilityService utilityRepository, IJwtService jwtService) : IUserService
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
                else if (result.IsLocked)
                {
                    return Result<string>.Failure("user account is locked");
                }
                var details = new
                {
                    userId = result.UserID,
                    userName = result.UserName,
                    Role = result.Role
                };

                var token = _jwtService.GenerateToken(details);

                return Result<string>.Success(token);
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<UserRoleDto>> GetRoleAsync(CancellationToken cancellationToken = default) {
            try {
                var data = await _userRepository.GetRole(cancellationToken);
                return data
                    .Select(x=> new UserRoleDto {
                        Id = x.PublicId?.ToString(),
                        Name = x.Role
                    })
                  //  .Where(x=> !x.Name!.Equals("Administrator",StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(x => x.Name)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<UserRole> GetRoleByIdAsync(string publicId,CancellationToken cancellationToken = default)
        {
            try
            {
                var roleList = await _userRepository.GetRole(cancellationToken);
                var data = roleList.FirstOrDefault(x => x.PublicId!.ToString() == publicId);
                if (data is null) {
                    throw new ArgumentException("Role not found.");
                }
                return data!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Result<string>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            try
            {

                var role = await GetRoleByIdAsync(request.RoleId,cancellationToken);
                int roleId = role.UserRoleId;
                var forRegisterData = new UserProfile(request.EmailAddress
                    , request.MobileNumber
                    , request.FirstName
                    , request.LastName
                    , request.Password
                    , request.DeviceId
                    , roleId);

                forRegisterData.ValidateUserProfile();
                forRegisterData.Password = _utilityRepository.HashPassword(request.EmailAddress, request.Password);
                await _userRepository.RegisterAsync(forRegisterData, cancellationToken);
                var result = "User registered successfully";
                return Result<string>.Success(result);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    return Result<string>.Failure(ex.Message);
                }
                throw;
            }
        }
    }
}
