using CleanFStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Application.Contracts.Persistence
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
