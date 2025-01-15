using ClothesShop.Data.Data;
using ClothesShop.Data.Data.Shop;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace ClothesShop.Intranet
{
    public static class DataSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var shopDbContext = new ClothesShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ClothesShopContext>>()))
            {

                if (!shopDbContext.Sizes.Any())
                {
                    var sized = new List<Size>()
                    {
                        new Size()
                        {

                           Name = "Small",
                            IsActive = true
                        },

                        new Size()
                        {

                            Name = "Medium",
                            IsActive = true
                        },
                        new Size()
                        {

                            Name = "Large",
                            IsActive = true
                        }
                    };
                    shopDbContext.Sizes.AddRange(sized);
                    shopDbContext.SaveChanges();
                }

                if (!shopDbContext.User.Any())
                {
                    var users = new List<User>()
                    {
                        new User()
                        {
                            Email = "marek@ds.sd",
                            Password = "123",
                            Username = "marek"
                        },
                        new User()
                        {
                            Email = "sas@da.sa",
                            Password = "123",
                            Username = "sas"
                        }
                    };
                    shopDbContext.User.AddRange(users);
                    shopDbContext.SaveChanges();
                }



            }
        }
    }

}
//https://stackoverflow.com/questions/72779826/trouble-with-seeding-data-in-asp-net-core-6