using AutoMapper;
using CleanFStore.Application.Contracts.Persistence;
using CleanFStore.Application.Mapping;
using CleanFStore.Domain.Entities.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Features.Products.Commands.CreateNewProduct
{
    public class CreateNewProductCommandHandler : IRequestHandler<CreateNewProductCommand, ServiceResponse<int>>
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public CreateNewProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<int>> Handle(CreateNewProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var _mapper = config.CreateMapper();
                var productEntity = _mapper.Map<Product>(request);
                await _productRepository.Insert(productEntity);
                return new ServiceResponse<int>
                {
                    Data = productEntity.ProductId,
                    StatusCode = 201,
                    Success = true,
                    Count = 0,
                    Message = "Successfully"
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }
    }
}
