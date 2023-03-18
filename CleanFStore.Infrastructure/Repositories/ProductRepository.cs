using CleanFStore.Application.Contracts.Persistence;
using CleanFStore.Domain.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanFStore.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(FStoreDBContext dbContext) : base(dbContext)
        {
        }
    }
}
