using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServicesLayer.Specifications;
using Shared;
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

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var specs = new ProductWithBrandAndTypeSpecification(id);

            var repo = _unitOfWork.GetRepository<Product, int>();
            return _mapper.Map<ProductDto>(await repo.GetByIdAsync(specs));
        }
        public async Task<IEnumerable<ProductDto>> GetProductsAsync(ProductQueryParams productQuery)
        {
            // Create Object from spcification
            var specs = new ProductWithBrandAndTypeSpecification(productQuery);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        #region Type and Brand
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            return _mapper.Map<IEnumerable<BrandDto>>(await repo.GetAllAsync());
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            return _mapper.Map<IEnumerable<TypeDto>>(await repo.GetAllAsync());
        } 
        #endregion
    }
}
