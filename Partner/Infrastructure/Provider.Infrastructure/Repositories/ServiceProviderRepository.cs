using Provider.Domain.Entities;
using Provider.Domain.Interfaces;
using Provider.Infrastructure.Persistence;
using Provider.Infrastructure.Persistence.Entities;

namespace Provider.Infrastructure.Repositories
{
    public class ServiceProviderRepository : IServiceProviderRepository
    {
        private readonly ServicesDbContext _servicesDbContext;
        public ServiceProviderRepository(ServicesDbContext servicesDbContext)
        {
            _servicesDbContext = servicesDbContext;
        }
        public async Task AddProviderDetailsAsync(ProviderDetails request, CancellationToken cancellationToken = default)
        {
            try {
                await using var transaction = await _servicesDbContext.Database.BeginTransactionAsync(cancellationToken);
                var providerDetails = new ServiceProviderDetails
                {
                    UserID = 0,
                    ProviderType = request.ProviderType,
                    BusinessName = request.BusinessName,
                    BusinessRegistrationNumber = request.BusinessRegistrationNumber,
                    Description = request.Description,
                    VerificationStatus = request.VerificationStatus,
                };

                await _servicesDbContext.AddAsync(providerDetails, cancellationToken);
                await _servicesDbContext.SaveChangesAsync(cancellationToken);

                var addressdetails = new Address
                {
                    AddressLine1 = request.Street,
                    AddressLine2 = request.Barangay,
                    City = request.CityOrMunicipal,
                    StateProvince = request.Province,
                    PostalCode = request.PostalCode,
                    DistrictRegion = request.Region,
                    CountryCode = request.CountryCode,
                };

                await _servicesDbContext.AddAsync(addressdetails, cancellationToken);
                await _servicesDbContext.SaveChangesAsync(cancellationToken);



                return;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
    }
}
