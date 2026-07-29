using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Domain.Interfaces;
using Service.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Infrastructure.Repositories
{
    public class GenericRepository(ServicesDbContext servicesDbContext) : IGenericRepository
    {
        private readonly ServicesDbContext _servicesDbContext = servicesDbContext;

        public async Task<EntityEntry<T>> AddAsync<T>(T item, CancellationToken cancellationToken = default) where T : class
        {
            return await _servicesDbContext.Set<T>().AddAsync(item, cancellationToken);
        }

        public Task<T?> GetEntityById<T>(int id, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _servicesDbContext.SaveChangesAsync(cancellationToken);
        }

        public Task UpdateAsync<T>(T item, CancellationToken cancellationToken = default) where T : class
        {
            throw new NotImplementedException();
        }


    }
}
