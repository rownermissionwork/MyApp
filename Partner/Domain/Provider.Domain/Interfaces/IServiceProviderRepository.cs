using Provider.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Domain.Interfaces
{
    public interface IServiceProviderRepository
    {
        Task AddProviderDetailsAsync(ProviderDetails request, CancellationToken cancellationToken = default);
    }
}
