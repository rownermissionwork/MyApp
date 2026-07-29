using Service.Application.Common;
using Service.Application.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<string>> AddCategory(AddCategoryRequest request, CancellationToken stoppingToken);
        Task<List<CategoriesDto>> GetAllAsync(CancellationToken stoppingToken);
    }
}