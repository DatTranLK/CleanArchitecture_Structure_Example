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

namespace CleanFStore.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand,ServiceResponse<string>>
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ServiceResponse<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productToUpdate = await _productRepository.GetById(request.ProductId);
                if(productToUpdate == null)
                {
                    return new ServiceResponse<string>
                    { 
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200,
                        Count = 0
                    };
                }
                var _mapper = config.CreateMapper();
                _mapper.Map(request, productToUpdate, typeof(UpdateProductCommand), typeof(Product));
                await _productRepository.Update(productToUpdate);
                return new ServiceResponse<string>
                { 
                    Message = "Successfully",
                    Success = true,
                    StatusCode = 204,
                    Count = 0
                };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
