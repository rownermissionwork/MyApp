using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.Domain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Domain.Interfaces
{
    public interface ICategoryRepository
    {
         Task<Category?> AddCategoryAsync(string name, CancellationToken cancellationToken = default);
         Task<List<Category>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
