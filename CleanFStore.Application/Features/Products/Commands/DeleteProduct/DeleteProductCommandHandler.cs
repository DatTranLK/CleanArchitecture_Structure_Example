using AutoMapper;
using CleanFStore.Application.Contracts.Persistence;
using CleanFStore.Application.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceResponse<string>>
    {
        private readonly IProductRepository _productRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<ServiceResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productToDelete = await _productRepository.GetById(request.ProductId);
                if (productToDelete == null)
                {
                    return new ServiceResponse<string>
                    {
                        Message = "No rows",
                        Success = true,
                        StatusCode = 200,
                        Count = 0
                    };
                }
                await _productRepository.Delete(productToDelete);
                return new ServiceResponse<string>
                { 
                    Message = "Successfully",
                    StatusCode = 204,
                    Success = true,
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
