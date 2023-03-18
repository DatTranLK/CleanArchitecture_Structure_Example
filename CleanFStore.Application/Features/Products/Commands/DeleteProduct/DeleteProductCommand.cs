using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<ServiceResponse<string>>
    {
        public int ProductId { get; set; }
    }
}
