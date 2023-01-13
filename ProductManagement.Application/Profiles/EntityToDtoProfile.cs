using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Profiles
{
    public class EntityToDtoProfile : Profile
    {
        public EntityToDtoProfile()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
