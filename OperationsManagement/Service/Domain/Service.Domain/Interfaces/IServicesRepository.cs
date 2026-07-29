using Service.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Domain.Interfaces
{
    public interface IServicesRepository
    {
        Task<Service.Domain.Entities.Service> AddServiceAsync(Service.Domain.Entities.Service request, CancellationToken cancellationToken = default);
        Task<List<Service.Domain.Entities.Service>> GetServicesByCategoryPublicIdAsync(string categoryPublicId,CancellationToken cancellationToken = default);
    }
}
