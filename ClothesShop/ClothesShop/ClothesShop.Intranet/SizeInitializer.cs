using ClothesShop.Data.Data;
using ClothesShop.Data.Data.CMS;
using ClothesShop.Data.Data.Shop;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ClothesShop.Intranet
{
    public static class SizeInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ClothesShopContext>();
                try
                {
                    context.Database.EnsureCreated();
                    var sizes = context.Sizes.FirstOrDefault();
                    //  if (sizes == null)
                    if (!context.Sizes.Any())
                    {
                        context.Sizes.AddRange(
                            new Size { IsActive = true, Name = "Small" },
                            new Size { IsActive = true, Name = "Medium" },
                            new Size { IsActive = true, Name = "Large" },
                            new Size { IsActive = true, Name = "XLarge" },
                            new Size { IsActive = true, Name = "XXLarge" },
                            new Size { IsActive = true, Name = "XXXLarge" }
                            );

                        context.SaveChanges();
                    }
                    if (!context.User.Any())
                    {
                        // Add default users
                        context.User.AddRange(
                            new User
                            {
                                Email = "kacper@gmail.com",
                                Password = "123",
                                Username = "kacpero"
                            },
                            new User
                            {
                                Email = "marek@ds.sd",
                                Password = "123",
                                Username = "marek"
                            },
                            new User
                            {
                                Email = "sas@da.sa",
                                Password = "123",
                                Username = "sas"
                            }
                        );
                        context.SaveChanges();
                    }
                    // Seed Categories if empty
                    if (!context.Category.Any())
                    {
                        var category1 = new Category
                        {
                            Name = "Category 1",
                            IsActive = true,
                            ImageNameUrl = "category1.jpg"
                        };

                        var category2 = new Category
                        {
                            Name = "Category 2",
                            IsActive = true,
                            ImageNameUrl = "category2.jpg"
                        };

                        context.Category.AddRange(category1, category2);
                        context.SaveChanges();
                    }

                    // Seed Products if empty
                    if (!context.Product.Any())
                    {
                        var category1 = context.Category.FirstOrDefault(c => c.Name == "Category 1");
                        if (category1 != null)
                        {
                            var product1 = new Product
                            {
                                Name = "Product 1",
                                Price = 9.99m,
                                ImageNameUrl = "product-1.jpg",
                                Description = "Product 1 description",
                                IsActive = true,
                                Color = "red",
                                Category = category1
                            };

                            var product2 = new Product
                            {
                                Name = "Product 2",
                                Price = 19.99m,
                                ImageNameUrl = "product-2.jpg",
                                Description = "Product 2 description",
                                IsActive = true,
                                Color = "blue",
                                Category = category1
                            };

                            context.Product.AddRange(product1, product2);
                        }

                        var category2 = context.Category.FirstOrDefault(c => c.Name == "Category 2");
                        if (category2 != null)
                        {
                            var product3 = new Product
                            {
                                Name = "Product 3",
                                Price = 29.99m,
                                ImageNameUrl = "product-3.jpg",
                                Description = "Product 3 description",
                                IsActive = true,
                                Color = "black",
                                Category = category2
                            };

                            var product4 = new Product
                            {
                                Name = "Product 4",
                                Price = 39.99m,
                                ImageNameUrl = "product-4.jpg",
                                Description = "Product 4 description",
                                IsActive = true,
                                Color = "yellow",
                                Category = category2
                            };

                            context.Product.AddRange(product3, product4);
                        }

                        context.SaveChanges();
                    }

                    if (!context.OrderStatus.Any())
                    {
                        var orderStatuses = new List<OrderStatus>
                        {
                            new OrderStatus { StatusName = "In Progress", IsActive = true },
                            new OrderStatus { StatusName = "Shipped", IsActive = true },
                            new OrderStatus { StatusName = "Delivered", IsActive = true },
                            new OrderStatus { StatusName = "Cancelled", IsActive = true },
                            new OrderStatus { StatusName = "Returned", IsActive = true },
                            new OrderStatus { StatusName = "Refunded", IsActive = true }
                        };

                        context.OrderStatus.AddRange(orderStatuses);
                        context.SaveChanges();
                    }
                    // Seed Banners if empty
                    if (!context.Banners.Any())
                    {
                        var banners = new List<Banner>
                        {
                            new Banner
                            {
                                Title = "Shop Smarter at Our Thrift Store",
                                SubTitle = "Find Quality Products at Bargain Prices",
                                Description = "<p>Discover a treasur<span style=\"background-color: rgb(255, 255, 255);\">e</span><span style=\"background-color: rgb(0, 0, 0);\"><span style=\"background-color: rgb(255, 255, 255);\"> trove</span></span> of gently used items at our thrift shop. From clothing to home decor, we offer high-quality products at unbeatable prices. Visit us today and start shopping <strong>smarter!</strong></p>",
                                ImageNameUrl = "/bootstrap/img/hero/hero-2.jpg",
                                ButtonText = "Shop now",
                                ButtonUrl = "/Shop/Index",
                                IsActive = true,
                                SocialLinks1 = "https://www.facebook.com/",
                                SocialIcon1 = null,
                                SocialLinks2 = "https://twitter.com/",
                                SocialIcon2 = null,
                                SocialLinks3 = "https://www.pinterest.com/",
                                SocialIcon3 = null,
                                SocialLinks4 = "https://www.instagram.com/",
                                SocialIcon4 = null
                            },
                            new Banner
                            {
                                Title = "Give Back and Shop at Our Thrift Store",
                                SubTitle = "Your Purchase Supports Local Causes",
                                Description = " When you shop at our thrift store, you're not just getting a great deal. You're also making a difference in your community. All proceeds from our sales go directly to supporting local causes. So come shop with us and give back while you save!",
                                ImageNameUrl = "/bootstrap/img/hero/hero-2.jpg",
                                ButtonText = "Shop now",
                                ButtonUrl = "/Shop/Index",
                                IsActive = true,
                                SocialLinks1 = "https://www.facebook.com/",
                                SocialIcon1 = null,
                                SocialLinks2 = "https://twitter.com/",
                                SocialIcon2 = null,
                                SocialLinks3 = "https://www.pinterest.com/",
                                SocialIcon3 = null,
                                SocialLinks4 = "https://www.instagram.com/",
                                SocialIcon4 = null
                            }
                        };

                        context.Banners.AddRange(banners);
                        context.SaveChanges();
                    }
                    // Seed Footer if empty
                    if (!context.Footers.Any())
                    {
                        var footers = new List<Footer>
                        {
                            new Footer
                            {
                                Title = "Thift Shop",
                                Url = null,
                                Icon = "/bootstrap/img/payment.png",
                                Logo = "/bootstrap/img/footer-logo.png",
                                Description = "Shop smart, shop thrift: Save money and the planet!",
                                ColumnNumber = 1
                            },
                            new Footer
                            {
                                Title = "Copyright",
                                Url = null,
                                Icon = null,
                                Logo = null,
                                Description = " Made by Kacper Obrzut",
                                ColumnNumber = 2
                            },
                            new Footer
                            {
                                Title = "Facebook",
                                Url ="https://www.facebook.com/" ,
                                Icon = "fa fa-facebook",
                                Logo = null,
                                Description = null,
                                ColumnNumber = 3
                            },
                            new Footer
                            {
                                Title = "Twitter",
                                Url ="https://twitter.com/" ,
                                Icon = "fa fa-twitter",
                                Logo = null,
                                Description = null,
                                ColumnNumber = 3
                            },
                            new Footer
                            {
                                Title = "Pinterest",
                                Url ="https://pl.pinterest.com/" ,
                                Icon = "fa fa-pinterest",
                                Logo = null,
                                Description = null,
                                ColumnNumber = 3
                            },
                            new Footer
                            {
                                Title = "Instagram",
                                Url ="https://www.instagram.com/" ,
                                Icon = "fa fa-instagram",
                                Logo = null,
                                Description = null,
                                ColumnNumber = 3
                            }
                        };

                        context.Footers.AddRange(footers);
                        context.SaveChanges();
                    }
                    // Seed MenuItems if empty
                    if (!context.MenuItems.Any())
                    {
                        var menuItems = new List<MenuItem>
                        {
                            new MenuItem
                            {
                                Link = "/Home/Index",
                                LinkName = "Home",
                                Controller = "Home",
                                AdditionalInfo = null,
                                Action = "Index",
                                Icon = null,
                                Order = 1,
                                IsActive = true,
                                RightMenu = false
                            },
                            new MenuItem
                            {
                                Link = "/Shop/Index",
                                LinkName = "Shop",
                                Controller = "Shop",
                                AdditionalInfo = null,
                                Action = "Index",
                                Icon = null,
                                Order = 2,
                                IsActive = true,
                                RightMenu = false
                            },
                            new MenuItem
                            {
                                Link = "/Checkout/Index",
                                LinkName = "Checkout",
                                Controller = "Checkout",
                                AdditionalInfo = null,
                                Action = "Index",
                                Icon = null,
                                Order = 3,
                                IsActive = true,
                                RightMenu = false
                            },
                            new MenuItem
                            {
                                Link = "#",
                                LinkName = "Search",
                                Controller = null,
                                AdditionalInfo = null,
                                Action = null,
                                Icon = "/bootstrap/img/icon/search.png",
                                Order = 4,
                                IsActive = true,
                                RightMenu = true
                            },
                            new MenuItem
                            {
                                Link = "/Cart/Index",
                                LinkName = "",
                                Controller = "Cart",
                                AdditionalInfo = "$0.00",
                                Action = "Index",
                                Icon = "/bootstrap/img/icon/cart.png",
                                Order = 5,
                                IsActive = true,
                                RightMenu = true
                            }
                        };

                        context.MenuItems.AddRange(menuItems);
                        context.SaveChanges();
                    }
                    if (!context.Order.Any())
                    {
                        bool entitiesExist = CheckEntitiesExistOrder(context, 1, 2);

                        if (entitiesExist)
                        {
                            var orders = new List<Order>
        {
            new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = "John Doe",
                Street = "123 Main St",
                City = "City",
                State = "State",
                ZipCode = "12345",
                UserId = 1,
                OrderStatusId = 1  // Specify the desired OrderStatusId
            },
            new Order
            {
                OrderDate = DateTime.Now,
                CustomerName = "Jane Smith",
                Street = "456 Elm St",
                City = "City",
                State = "State",
                ZipCode = "54321",
                UserId = 2,
                OrderStatusId = 2  // Specify the desired OrderStatusId
            },
            // Add more sample Order objects as needed
        };

                            context.Order.AddRange(orders);
                            context.SaveChanges();
                        }
                    }
                    if (!context.OrderProduct.Any())
                    {
                        bool entitiesExist = CheckEntitiesExist(context, 2);

                        if (entitiesExist)
                        {
                            var orderProducts = new List<OrderProduct>
                            {
                                //new OrderProduct
                                //{
                                //    OrderId = 1,
                                //    ProductId = 1,
                                //    Quantity = 2,
                                //    Discount = 10,
                                //    IdSize = 1,
                                //    TotalPrice = 100.00m
                                //},
                                new OrderProduct
                                {
                                    OrderId = 2,
                                    ProductId = 2,
                                    Quantity = 1,
                                    Discount = null,
                                    IdSize = 2,
                                    TotalPrice = 50.00m
                                },
                                // Add more sample OrderProduct objects as needed
                            };

                            context.OrderProduct.AddRange(orderProducts);
                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                return app;
            }
        }
        private static bool CheckEntitiesExist(ClothesShopContext context, params int[] entityIds)
        {
            foreach (int entityId in entityIds)
            {
                bool orderExists = context.Order.Any(o => o.OrderId == entityId);
                bool productExists = context.Product.Any(p => p.ProductId == entityId);
                bool sizeExists = context.Sizes.Any(s => s.Id == entityId);

                if (!(orderExists && productExists && sizeExists))
                {
                    return false;
                }
            }

            return true;
        }
        private static bool CheckEntitiesExistOrder(ClothesShopContext context, params int[] entityIds)
        {
            foreach (int entityId in entityIds)
            {
                bool userExists = context.User.Any(o => o.UserId == entityId);
                bool orderStatusExists = context.OrderStatus.Any(os => os.OrderStatusId == entityId);

                if (!( userExists || orderStatusExists))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
//https://github.com/techwithpat/MovieApp/blob/master/MovieApp.Data/Movie.cs