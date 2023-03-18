using AutoMapper;
using CleanFStore.Application.Features.Products.Commands.CreateNewProduct;
using CleanFStore.Application.Features.Products.Commands.UpdateProduct;
using CleanFStore.Application.Features.Products.Queries.GetProductsList;
using CleanFStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductsResponse>().ForMember(dto => dto.CategoryName, act => act.MapFrom(obj => obj.Category.CategoryName)).ReverseMap();
            CreateMap<Product, CreateNewProductCommand>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();
        }
    }
}
