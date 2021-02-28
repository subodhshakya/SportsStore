using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent
    {
        /// <summary>
        /// This method is called when the component is used in a Razor view
        /// and the result of the Invoke method is inserted into the HTML sent to the browser.
        /// This component is for category list and for it to appear on all pages, use the view
        /// component in shared layout, rather than specific view.
        /// </summary>
        /// <returns></returns>
        public string Invoke()
        {
            return "Hello from the Nav View Component";
        }
    }
}
