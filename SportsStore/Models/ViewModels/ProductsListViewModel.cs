using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }

        /// <summary>
        /// Current Category will help render sidebar
        /// </summary>
        public string CurrentCategory { get; set; }
    }
}
