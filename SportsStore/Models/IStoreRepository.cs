using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface IStoreRepository
    {
        // Allows to obtain sequence of Product objects.
        // IQueryable derives from IEnumerable<T> interface and represents a collection
        // of objects that can be queried.
        // Class that depends on IProductRepository interface can obtain Product objects
        // without needing to know the details of how they are stored or implemented.
        IQueryable<Product> Products { get; }
    }
}
