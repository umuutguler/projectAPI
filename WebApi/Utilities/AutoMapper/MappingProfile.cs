using AutoMapper;
using Entities.Models;
using Entities.DataTransferObjects;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile // Kaynaktan hedefe id, title, price değerlerini mapleyebilmek için MappingProfile ihtiyaç var
    {
        public MappingProfile()
        {
            CreateMap<ProductDtoForUpdate, Product>();  // CreateMap<Tsorce, TDestination>
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDtoForInsertion , Product>();
        }
    }
}