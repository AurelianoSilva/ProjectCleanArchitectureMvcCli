using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;

namespace CleanArchMvc.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository ?? 
                throw new ArgumentNullException(nameof(categoryRepository));

            this.mapper = mapper;
        }

        public async Task Add(CategoryDTO categoryDTO)
        {
            var categoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.CreateAsync(categoryEntity);
        }
        public async Task Update(CategoryDTO categoryDTO)
        {
            var categoryEntity = mapper.Map<Category>(categoryDTO);
            await categoryRepository.UpdateAsync(categoryEntity);
        }

        public async Task Delete(int? id)
        {
            var categoryEntity = categoryRepository.GetByIdAsync(id).Result;
            await categoryRepository.RemoveAsync(categoryEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
        {
            var categoriesEntity = await categoryRepository.GetCategoriesAsync();
            return mapper.Map<IEnumerable<CategoryDTO>>(categoriesEntity);
        }

        public async Task<IEnumerable<CategoryDTO>> GetByIdAsync(int? id)
        {
            var categoryEntity = await categoryRepository.GetByIdAsync(id);
            return mapper.Map<IEnumerable<CategoryDTO>>(categoryEntity);
        }
    }
}
