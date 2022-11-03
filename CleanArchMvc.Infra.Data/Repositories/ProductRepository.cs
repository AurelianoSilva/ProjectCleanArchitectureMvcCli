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
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        public ProductRepository(ApplicationDbContext _productContext)
        {
            this._productContext = _productContext;
        }
        public async Task<Product> CreateAsync(Product product)
        {
            _productContext.Products.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //eager loading (carregamento adiantado)
            return await _productContext.Products.Include(c => c.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Metodo retona o produto pelo id e a categoria relacionada ao produto.
        /// Utilizado o metodo de carregamento adintado pelo metodo include do entity framework core
        /// </summary>
        /// <param name="id">id do produto</param>
        /// <returns>retorna o produto e a categoria do produto</returns>
        //public async Task<Product> GetProductCategoryAsync(int? id)
        //{
        //    //eager loading (carregamento adiantado)
        //    return await _productContext.Products.Include(c => c.Category)
        //        .SingleOrDefaultAsync(p => p.Id == id);
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync() => await _productContext.Products.ToListAsync();

        public async Task<Product> RemoveAsync(Product product)
        {
            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Products.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
    }
}
