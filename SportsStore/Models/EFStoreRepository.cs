using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class EFStoreRepository : IStoreRepository
    {
        private StoreDbContext context;

        public EFStoreRepository(StoreDbContext ctx)
        {
            this.context = ctx;
        }

        // Products property in context returns DbSet<Product> object,
        // which implements the IQueryable<T> interface and makes it easy to implement the 
        // repository interface when using EF Core
        public IQueryable<Product> Products => context.Products;
    }
}
