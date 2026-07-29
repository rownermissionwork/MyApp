
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

namespace Service.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ServicesDbContext _servicesDbContext;
        public CategoryRepository(ServicesDbContext servicesDbContext)
        {
            _servicesDbContext = servicesDbContext;
        }

        public async Task<Category?> AddCategoryAsync(string name, CancellationToken cancellationToken)
        {
            var data = new Category();
            var request = new Categories
            {
                Name = name,
                IsActive = true
            };
            await _servicesDbContext.Categories.AddAsync(request, cancellationToken);
            await _servicesDbContext.SaveChangesAsync(cancellationToken);

            data.Id = request.Id;
            data.Name = request.Name;
            data.IsActive = request.IsActive;

            return data;
        }



        public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _servicesDbContext.Categories
                .Where(x => x.IsActive)
                .Select(x => new Category { Name = x.Name, Id = x.Id,PublicId = x.PublicId })
                .ToListAsync(cancellationToken);
        }


    }
}
