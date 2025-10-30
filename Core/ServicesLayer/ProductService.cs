using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ProductService(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            return _mapper.Map<IEnumerable<BrandDto>>(await repo.GetAllAsync());
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var repo = _unitOfWork.GetRepository<Product, int>();
            return _mapper.Map<ProductDto>(await repo.GetByIdAsync(id));
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            return _mapper.Map<IEnumerable<TypeDto>>(await repo.GetAllAsync());
        }
    }
}
