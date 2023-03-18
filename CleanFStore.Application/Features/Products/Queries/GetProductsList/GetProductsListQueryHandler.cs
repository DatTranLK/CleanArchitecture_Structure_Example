using AutoMapper;
using CleanFStore.Application.Contracts.Persistence;
using CleanFStore.Application.Mapping;
using CleanFStore.Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Features.Products.Queries.GetProductsList
{
    public class GetProductsListQueryHandler : IRequestHandler<GetProductsListQuery, ServiceResponse<List<ProductsResponse>>>
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public GetProductsListQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<List<ProductsResponse>>> Handle(GetProductsListQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<Expression<Func<Product, object>>> includes = new List<Expression<Func<Product, object>>>
                {
                    x => x.Category
                };
                var lst = await _productRepository.GetAllWithCondition(null, includes, x => x.ProductId, true);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<List<ProductsResponse>>(lst);
                if(lst.Count() <= 0)
                {
                    return new ServiceResponse<List<ProductsResponse>>
                    {
                        Success = true,
                        StatusCode = 200,
                        Message = "No rows",
                        Count = lst.Count()
                    };
                }
                return new ServiceResponse<List<ProductsResponse>>
                {
                    Data = lstDto,
                    Success = true,
                    StatusCode = 200,
                    Message = "Successfully",
                    Count = lstDto.Count
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
