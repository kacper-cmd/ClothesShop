using ClothesShop.Data.Data.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClothesShop.Data.Data.CMS;

namespace ClothesShop.Data.Data
{
    
    public class ClothesShopContext : DbContext
    {
        public ClothesShopContext(DbContextOptions<ClothesShopContext> options)
        : base(options)
        {
        }

       
        public DbSet<Order> Order { get; set; } = default!;
        public DbSet<OrderProduct> OrderProduct { get; set; } = default!;
        public DbSet<OrderStatus> OrderStatus { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
   
        public DbSet<User> User { get; set; } = default!;
        public DbSet<Banner> Banners { get; set; } = default!;
        public DbSet<MenuItem> MenuItems { get; set; } = default!;
        public DbSet<Footer> Footers { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<Size> Sizes { get; set; } = default!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }


    }
}
