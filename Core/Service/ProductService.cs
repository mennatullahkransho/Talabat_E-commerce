using AutoMapper;
using DomainLayer.Interfaces;
using DomainLayer.Models;
using ServiceAbstraction;
using Shared.DataTransferObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Specifications;
using Shared;

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
            return mapper.Map<Product, ProductDto>(Product);

        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync(int? BrandId, int? TypeId, ProductsSortingOptions sortingOption)
        {
            var specification = new ProductWithBrandAndTypeSpecification(BrandId,TypeId ,sortingOption);
            var Products = await unitOfWork.GetRepository<Product, int>().GetAllAsync(specification);
            var ProductsDto = mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Products);
            return ProductsDto;
        }

        public async Task<IEnumerable<TypeDto>> GetTypesAsync()
        {
            var Types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var TypesDto = mapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(Types);
            return TypesDto;
        }
    }
}
