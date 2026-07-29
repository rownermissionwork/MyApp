using Account.Domain.Entities;
using Account.Domain.Interfaces;
using Account.Infrastructure.Persistence;
using Account.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Infrastructure.Repositories
{
    public class UserRepository(AccountDbContext context) : IUserRepository
    {
        private readonly AccountDbContext _authDbContext = context;

        public async Task<UserLogin?> GetUserByUserNameAsync(string userName)
        {
            try
            {
                var data = await (from u in _authDbContext.User.AsNoTracking()
                                  join up in _authDbContext.UserProfiles.AsNoTracking() on u.UserID equals up.UserId
                                  join r in _authDbContext.UserRoles.AsNoTracking() on up.UserRoleID equals r.UserRoleId
                                  where u.Email == userName || u.MobileNumber == userName
                                  select new UserLogin
                                  {
                                      UserID = u.UserID,
                                      UserName = u.Email,
                                      PasswordHash = u.PasswordHash,
                                      IsActive = u.IsActive,
                                      IsLocked = u.IsLocked,
                                      Role = r.RoleName,

                                  }).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task RegisterAsync(Domain.Entities.UserProfile request,CancellationToken cancellationToken = default)
        {
            await using var transaction = await _authDbContext.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var user = new Persistence.Entities.Users
                {
                    Email = request.EmailAddress,
                    MobileNumber = request.MobileNumber,
                    PasswordHash = request.Password,
                    DeviceID =  request.DeviceId,
                    IsActive = true,
                    IsLocked = false,
                    CreatedAt = DateTime.UtcNow
                };
                await _authDbContext.User.AddAsync(user,cancellationToken);

                await _authDbContext.SaveChangesAsync(cancellationToken);

                var profile = new Persistence.Entities.UserProfile
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    UserId = user.UserID,
                    PublicId = Guid.NewGuid(),
                    AddressId = 0,
                    UserRoleID = request.UserRoleId,
                    IsActive = true

                };

                await _authDbContext.UserProfiles.AddAsync(profile, cancellationToken);

                await _authDbContext.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<List<Domain.Entities.UserRole>> GetRole(CancellationToken cancellationToken = default)
        {
            try {
                return await _authDbContext.UserRoles
                    .Where(x => x.IsActive)
                    .Select(x => new Domain.Entities.UserRole
                    {
                        PublicId = x.PublicId.ToString(), 
                        Role = x.RoleName,
                        UserRoleId = x.UserRoleId
                    })
                    .ToListAsync(cancellationToken);
            } 
            catch (Exception)
            {
                throw;
            }

        }
    }
}
