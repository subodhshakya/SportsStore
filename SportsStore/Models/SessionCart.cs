using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    /// <summary>
    /// This subclass overrides AddItem, RemoveLine and Clear methods
    /// so that they call the base implementation and then store the
    /// updated state in the session using the extension methods on
    /// ISession interface.    
    /// </summary>
    public class SessionCart : Cart
    {
        /// <summary>
        /// This is factory for creating SessionCart objects and providing
        /// them with an ISession object so they can store themselves.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static Cart GetCart(IServiceProvider services)
        {
            // ?. is null conditional operator: "Evaluate the first operand; if that's null, stop, with a result of null.
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart") ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.SetJson("Cart", this);
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
