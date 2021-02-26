using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class SeedData
    {
        // This method receives an IApplicationBuilder argument,
        // which is the interface used in the Configure method of the 
        // Startup class to register middleware component to handle HTTP requests.
        // EnsurePopulated method obtains a StoreDbContext object through the 
        // IApplicationBuilder interface and callthe Database.Migrate method if there are
        // any pending migrations.
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            //if (context.Products == null || !context.Products.Any())
            using(context)
            {
                context.Products.AddRange(
                        new Product { 
                            Name = "Kayak",
                            Description = "A boat for one person",
                            Category = "Watersports",
                            Price = 275
                        },
                        new Product
                        {
                            Name = "Lifejacket",
                            Description = "Protective and fashionable",
                            Category = "Watersports",
                            Price = 48.95m
                        },
                        new Product
                        {
                            Name = "Soccer Ball",
                            Description = "FIFA-approved size and weight",
                            Category = "Soccer",
                            Price = 19.5m
                        }
                    );
                context.SaveChanges();
                context.Dispose();
            }
        }

        //public static void Seed(StoreDbContext context)

        //{
        //    using (context)
        //    {
        //        context.AddRange(
        //                new Product
        //                {
        //                    Name = "Kayak",
        //                    Description = "A boat for one person",
        //                    Category = "Watersports",
        //                    Price = 275
        //                },
        //                new Product
        //                {
        //                    Name = "Lifejacket",
        //                    Description = "Protective and fashionable",
        //                    Category = "Watersports",
        //                    Price = 48.95m
        //                },
        //                new Product
        //                {
        //                    Name = "Soccer Ball",
        //                    Description = "FIFA-approved size and weight",
        //                    Category = "Soccer",
        //                    Price = 19.5m
        //                }
        //            );
        //        context.SaveChanges();
        //        context.Dispose();
        //    }
        //}
    }
}
