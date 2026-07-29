using Provider.Application.Common;
using Provider.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provider.Application.Interfaces
{
    public interface IServiceProviderService
    {
        public Task<Result<string>> EditProviderDetailsAsync(int userId, ProviderDetailsRequest request, CancellationToken cancellationToken);
    }
}
