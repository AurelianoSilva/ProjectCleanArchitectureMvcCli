using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private ApplicationDbContext _categoryContext;
        public CategoryRepository(ApplicationDbContext _categoryContext)
        {
            this._categoryContext = _categoryContext;
        }

        public async Task<Category> CreateAsync(Category category)
        {
            //armazenando apenas em memória
            _categoryContext.Add(category);
            //Salvando as informações no banco de dados
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> GetByIdAsync(int? id) => await _categoryContext.Categories.FindAsync(id);

        public async Task<IEnumerable<Category>> GetCategoriesAsync() => await _categoryContext.Categories.ToListAsync();

        public async Task<Category> RemoveAsync(Category category)
        {
            //Removendo apenas em memória
            _categoryContext.Remove(category);
            //Removendo o category no banco de dados
            await _categoryContext.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            //Atualizando em memória
            _categoryContext.Update(category);
            //Atualizando no banco de dados
            await _categoryContext.SaveChangesAsync();
            return category;
        }
    }
}
