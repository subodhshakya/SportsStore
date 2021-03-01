using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IStoreRepository repository;

        /// <summary>
        /// Input parameter is IStoreRepository, when ASP.NET Core needs to
        /// create an instance of view component class, it wll note the need to 
        /// provide the value of this parameter and inspect the configuration in the 
        /// Startup class to determine which implementation object should be used.
        /// This is the dependency injection feature.
        /// </summary>
        /// <param name="repo"></param>
        public NavigationMenuViewComponent(IStoreRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// This method is called when the component is used in a Razor view
        /// and the result of the Invoke method is inserted into the HTML sent to the browser.
        /// This component is for category list and for it to appear on all pages, use the view
        /// component in shared layout, rather than specific view.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
