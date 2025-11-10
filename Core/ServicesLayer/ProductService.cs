using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.Product;
using ServicesLayer.Specifications;
using Shared;
using Shared.DTOs.ProductDtos;
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

            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(specs);
            if(product is null)throw new ProductNotFoundException(id);

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParams productQuery)
        {
            // Create Object from spcification
            var repo = _unitOfWork.GetRepository<Product, int>();
            var specs = new ProductWithBrandAndTypeSpecification(productQuery);
            var products = await repo.GetAllAsync(specs);
            var mapedProduct = _mapper.Map<IEnumerable<ProductDto>>(products);
            // Get Total Count
            var countSpecs = new ProductCountSpecification(productQuery);
            var Count = await repo.CountAsync(countSpecs);

            var returnObj = new PaginatedResult<ProductDto>(productQuery.PageIndex, productQuery.PageSize , Count , mapedProduct);
            return returnObj;
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
