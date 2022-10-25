using CleanArchMvc.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetCategoriesAsync();
        Task<IEnumerable<CategoryDTO>> GetByIdAsync(int? id);
        Task Add(CategoryDTO categoryDTO);
        Task Update(CategoryDTO categoryDTO);
        Task Delete(int? id);
    }
}
