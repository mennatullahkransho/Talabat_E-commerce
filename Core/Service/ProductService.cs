using AutoMapper;
using DomainLayer.Interfaces;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Specifications;
using Shared;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Shared.DataTransferObjets.ProductModuleDto;

namespace Service
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var Brands= await unitOfWork.GetRepository<ProductBrand,int>().GetAllAsync();
            var BransDto= mapper.Map<IEnumerable<ProductBrand>,IEnumerable<BrandDto>>(Brands);
            return BransDto;
        }

        public async Task<ProductDto> GetProductByIdAsync(int Id)
        {
            var specification = new ProductWithBrandAndTypeSpecification(Id);
            var Product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(specification);
            if(Product==null)
            {
                throw new ProductNotFoundException(Id);
            }
            return mapper.Map<Product, ProductDto>(Product);

        }

        public async Task<PaginatedResult<ProductDto>> GetProductsAsync(ProductQueryParams queryParams)
        {
            var Repo = unitOfWork.GetRepository<Product, int>();
            var specification = new ProductWithBrandAndTypeSpecification(queryParams);
            var Products = await Repo.GetAllAsync(specification);
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            var ProductCount = Products.Count();
            var CountSpec = new ProductCountSpecification(queryParams);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, ProductCount, TotalCount, ProductsDto);
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }
    }
}
