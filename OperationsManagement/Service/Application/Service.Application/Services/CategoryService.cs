using Service.Application.Common;
using Service.Application.Dtos.Category;
using Service.Application.Interfaces;
using Service.Domain.Entities;
using Service.Domain.Interfaces;
using System.Collections.Generic;


namespace Service.Application.Services
{
    public class CategoryService : ICategoryService 
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) {
            _categoryRepository = categoryRepository;
        }
        public async Task<Result<string>> AddCategory(Dtos.Category.AddCategoryRequest request, CancellationToken stoppingToken)
        {
            try {

                var category = new Category();

                category.ValidateCategory(request.Category);

                await _categoryRepository.AddCategoryAsync(request.Category, stoppingToken);
                var result = "Category added successfully";
                return Result<string>.Success(result);
            }
            catch (Exception ex)
            {
               
                if (ex is ArgumentException) {

                    return Result<string>.Failure(ex.Message);
                }
                throw;
            }
        }
        public async Task<List<CategoriesDto>> GetAllAsync(CancellationToken stoppingToken)
        {
            try {
                var data = await _categoryRepository.GetAllAsync(stoppingToken);
                var result =  data.Select(c => new CategoriesDto { PublicId = c.PublicId.ToString(), Name = c.Name }).ToList();
                return result;
            }
            catch (Exception) {
                throw;
            }
            
            
        }
    }
}
