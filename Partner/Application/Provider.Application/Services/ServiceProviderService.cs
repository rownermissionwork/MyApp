using Provider.Application.Common;
using Provider.Application.Dtos;
using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Services
{
    public class ServiceProviderService : Application.Interfaces.IServiceProviderService
    {
        public async Task<Result<string>> EditProviderDetailsAsync(int userId, ProviderDetailsRequest request, CancellationToken cancellationToken)
        {
            try {
                var data = new ProviderDetails
                    (userId,
                    request.FirstName,
                    request.LastName,
                    request.Phone,
                    request.ProviderType,
                    request.BusinessName,
                    request.BusinessRegistrationNumber,
                    request.Description,
                    request.YearsOfExperience,
                    request.Street,
                    request.Barangay,
                    request.CityOrMunicipal,
                    request.Province ?? string.Empty,
                    request.PostalCode,
                    request.Region,
                    request.CountryCode,
                    1
                    );

                data.ValidateProviderDetails();
                var result = "Details successfully saved.";

                return Result<string>.Success(result);
            }
            catch (Exception ex) {
                if (ex is ArgumentException)
                {
                    return Result<string>.Failure(ex.Message);
                }
                throw;
            }
        }
    }
}
