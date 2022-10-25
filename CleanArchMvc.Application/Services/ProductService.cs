using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository ??
                 throw new ArgumentNullException(nameof(productRepository));

            this.mapper = mapper;
        }

        public async Task Add(ProductDTO productDTO)
        {
            var productEntity = mapper.Map<Product>(productDTO);
            await productRepository.CreateAsync(productEntity);
        }

        public async Task Update(ProductDTO productDTO)
        {
            var productEntity = mapper.Map<Product>(productDTO);
            await productRepository.CreateAsync(productEntity);
        }

        public async Task Delete(int? id)
        {
            var productEntity = productRepository.GetByIdAsync(id).Result;
            await productRepository.RemoveAsync(productEntity);
        }

        public async Task<ProductDTO> GetByIdAsync(int? id)
        {
            var productsEntity = await productRepository.GetByIdAsync(id);
            return mapper.Map<ProductDTO>(productsEntity);
        }

        public async Task<ProductDTO> GetProductCategory(int? id)
        {
            var productEntity = await productRepository.GetProductCategoryAsync(id);
            return mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsEntity = await productRepository.GetProductsAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(productsEntity);
        }
    }
}
