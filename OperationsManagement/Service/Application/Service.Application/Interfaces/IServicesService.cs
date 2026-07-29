using Service.Application.Common;
using Service.Application.Dtos.Category;
using Service.Application.Dtos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.Interfaces
{
    public interface IServicesService
    {
        Task<Result<string>> AddService(AddServiceRequest request, CancellationToken stoppingToken);
        Task<List<ServicesDto>> GetServicesByCategoryPublicIdAsync(string categoryPublicId, CancellationToken stoppingToken);
    }
}
