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

            if (!context.Products.Any())
            {
                using (context)
                {
                    context.Products.AddRange(
                            new Product
                            {
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
                            },
                            new Product
                            {
                                Name = "Corner Flags",
                                Description = "Give your playing field a professional touch",
                                Category = "Soccer",
                                Price = 34.95m
                            },
                            new Product
                            {
                                Name = "Stadium",
                                Description = "Flat-packed 35,000-seat stadium",
                                Category = "Soccer",
                                Price = 79500
                            },
                            new Product
                            {
                                Name = "Thinking Cap",
                                Description = "Improve brain efficiency by 75%",
                                Category = "Chess",
                                Price = 16
                            },
                            new Product
                            {
                                Name = "Unsteady Chair",
                                Description = "Secretlhy give your opponent a disadvantage",
                                Category = "Chess",
                                Price = 29.95m
                            },
                            new Product
                            {
                                Name = "Human Chess Board",
                                Description = "A fun game for the family",
                                Category = "Chess",
                                Price = 75
                            },
                            new Product
                            {
                                Name = "Bling-Bling King",
                                Description = "Gold-plated, diamond-studded King",
                                Category = "Chess",
                                Price = 1200
                            },
                            new Product
                            {
                                Name = "Chess Timer",
                                Description = "Time keeping device for chess game",
                                Category = "Chess",
                                Price = 39.99m
                            }
                        );
                    context.SaveChanges();
                    context.Dispose();
                }
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
