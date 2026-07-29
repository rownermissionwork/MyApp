using Microsoft.EntityFrameworkCore;
using Service.Domain.Entities;
using Service.Domain.Interfaces;
using Service.Infrastructure.Persistence;
using Service.Infrastructure.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Infrastructure.Repositories
{
    public class ServicesRepository : IServicesRepository
    {
        private readonly ServicesDbContext _servicesDbContext;
        public ServicesRepository(ServicesDbContext servicesDbContext) { 
            _servicesDbContext = servicesDbContext;
        }
        public async Task<Domain.Entities.Service> AddServiceAsync(Domain.Entities.Service request, CancellationToken cancellationToken = default)
        {
            var addService = new Services { 
                CategoryId = request.CategoryId,
                Service = request.Name!,
                CreatedBy = request.CreatedBy!,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            await _servicesDbContext.Services.AddAsync(addService, cancellationToken);
            await _servicesDbContext.SaveChangesAsync(cancellationToken);
            var data = new Domain.Entities.Service {
                CategoryId = request.CategoryId,
                Name = request.Name,
                CreatedBy = request.CreatedBy,
                Id = addService.Id,
                PublicId = request.PublicId,
            };


            return data;
        }

        public async Task<List<Domain.Entities.Service>> GetServicesByCategoryPublicIdAsync(string categoryPublicId, CancellationToken cancellationToken = default)
        {
            var data = await (from c in _servicesDbContext.Categories.AsNoTracking()
                              join s in _servicesDbContext.Services.AsNoTracking() on c.Id equals s.CategoryId
                              where c.PublicId.ToString() == categoryPublicId
                              select new Domain.Entities.Service {
                                  PublicId = s.PublicId,
                                  Name = s.Service,
                              }).ToListAsync(cancellationToken);

            return data;

        }
    }
}
