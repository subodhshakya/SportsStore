using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SportsStore.Infrastructure;
using SportsStore.Models;

namespace SportsStore.Pages
{
    /// <summary>
    /// This is called Page model.
    /// Razor pages are complementary to the MVC Framework, and 
    /// I find myself using them alongside controllers and views
    /// because they are well-suited to self-contained features that
    /// don't require the complexity of the MVC Framework. I describe
    /// Razor Pages in Ch 23 and show their use alongside controllers
    /// throughout Part 3 and Part 4 of this book.
    /// </summary>
    public class CartModel : PageModel
    {
        private IStoreRepository repository;

        public CartModel(IStoreRepository repo) {
            repository = repo;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }


        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(product, 1);
            HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
