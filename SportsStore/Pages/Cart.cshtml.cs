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

        public CartModel(IStoreRepository repo, Cart cartService) {
            repository = repo;
            
            // The page model indicates that it needs a Cart object by declaring a 
            // constructor argument, which has allowed me to remove the statements
            // that load and store sessions from the handler method.
            // Since services are available throughout the application, any
            // component can get hold of the user's cart using the same technique.
            Cart = cartService;
        }

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; }


        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";            
        }

        public IActionResult OnPost(long productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            Cart.AddItem(product, 1);
            //HttpContext.Session.SetJson("cart", Cart);
            return RedirectToPage(new { returnUrl = returnUrl });
        }

        /// <summary>
        /// This is handler method for "Remove" from cshtml page
        /// During the matching process, OnPost keyword is ignored
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public IActionResult OnPostRemove(long productId, string returnUrl)
        {
            Cart.RemoveLine(Cart.Lines.First(cl => cl.Product.ProductID == productId).Product);
            return RedirectToPage(new { returnUrl = returnUrl });
        }
    }
}
