using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClothesShop.Data.Data
{
   
    public class ClothesShopContextFactory : IDesignTimeDbContextFactory<ClothesShopContext>
    {

        public ClothesShopContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ClothesShopContext>();
            optionsBuilder.UseSqlServer("Server=KACPER;Database=ClothesShopDB;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ClothesShopContext(optionsBuilder.Options);
        }

    }
}
