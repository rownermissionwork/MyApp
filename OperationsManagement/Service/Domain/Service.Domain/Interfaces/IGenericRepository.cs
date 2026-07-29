using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Domain.Interfaces
{
    public interface  IGenericRepository
    {
        Task<EntityEntry<T>> AddAsync<T>(T item, CancellationToken cancellationToken = default) where T : class;
        Task UpdateAsync<T>(T item, CancellationToken cancellationToken = default) where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T?> GetEntityById<T>(int id, CancellationToken cancellationToken = default) where T : class;
    }
}
