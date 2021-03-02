using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository repository;
        public int PageSize = 4;

        public HomeController(IStoreRepository repo)
        {
            this.repository = repo;
        }

        //public IActionResult Index()
        //{
        //    return View(this.repository.Products);
        //}

        //public ViewResult Index(int productPage = 1)
        //=> View(repository.Products
        //    .OrderBy(p => p.ProductID)
        //    .Skip((productPage - 1) * PageSize)
        //    .Take(PageSize));

        /// <summary>
        /// This function pass a ProductsListViewModel object as the model data to the view.
        /// </summary>
        /// <param name="productPage"></param>
        /// <returns></returns>
        public ViewResult Index(string category, int productPage = 1)
        => View(new ProductsListViewModel
        {
            Products = repository.Products
            .Where(p => p.Category == null || p.Category == category)
            .OrderBy(p => p.ProductID)
            .Skip((productPage - 1) * PageSize)
            .Take(PageSize),
            PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = category == null ?
                repository.Products.Count() :
                repository.Products.Where(e => e.Category == category).Count()
            }
        });

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
