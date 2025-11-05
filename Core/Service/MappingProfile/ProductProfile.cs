using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObjets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfile
{
    public class ProductProfile :Profile
    {
        ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist=>dist.BrandName,options=>options.MapFrom(src=>src.ProductBrand.Name))
                .ForMember(dist => dist.TypeName, options => options.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
