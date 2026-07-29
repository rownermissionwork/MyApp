using Service.Application.Common;
using Service.Application.Dtos.Category;
using Service.Application.Dtos.Services;
using Service.Application.Interfaces;
using Service.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServicesRepository _servicesRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ServicesService(IServicesRepository servicesRepository, ICategoryRepository categoryRepository)
        {
            _servicesRepository = servicesRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<string>> AddService(AddServiceRequest request, CancellationToken stoppingToken)
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync(stoppingToken);
                var category = categories.FirstOrDefault(x => x.PublicId.ToString().Equals(request.CategoryPublicId, StringComparison.CurrentCultureIgnoreCase));

                if (category is null)
                {
                    return Result<string>.Failure("Category Public Id not found.");
                }
                var serviceData = await _servicesRepository.GetServicesByCategoryPublicIdAsync(request.CategoryPublicId, stoppingToken); 
                if ( serviceData is not null && serviceData.Any(x=>x.Name!.Equals(request.ServiceName,StringComparison.CurrentCultureIgnoreCase))) {
                    return Result<string>.Failure("Service Name already exist.");
                }
                var services = new Domain.Entities.Service();
                services.ValidateService(category!.Id, request.ServiceName, request.CreatedBy);
                await _servicesRepository.AddServiceAsync(services, stoppingToken);
                var result = "Service added successfully";
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

        public async Task<List<ServicesDto>> GetServicesByCategoryPublicIdAsync(string categoryPublicId, CancellationToken stoppingToken)
        {
            try
            {
                var data = await _servicesRepository.GetServicesByCategoryPublicIdAsync(categoryPublicId, stoppingToken);
                var result = data.Select(c => new ServicesDto { PublicId = c.PublicId.ToString(), ServiceName = c.Name }).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
