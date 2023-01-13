using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Commands;

namespace ProductManagement.Application.Profiles
{
    public class DtoToCommandProfile : Profile
    {
        public DtoToCommandProfile() { 
            CreateMap<ProductDto, CreateProductCommand>();
        }
    }
}
