using arapi.Dtos.Product;
using ARAPI.Dtos.Product;
using AutoMapper;

namespace arapi
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, GetProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<UpdateProductDto,Product>();
        }
    }
}
