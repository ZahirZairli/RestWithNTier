using AutoMapper;
using Entities;
using Entities.Dtos;

namespace Business.Utilities.Profiles;
public class ProductProfile:Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, GetProductDto>();
        CreateMap<UpdateProductDto, Product>();
    }
}
