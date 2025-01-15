using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data;

public partial class RagsToRichesDbContext : DbContext
{
    public RagsToRichesDbContext()
    {
    }

    public RagsToRichesDbContext(DbContextOptions<RagsToRichesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Footer> Footers { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<MenuItem> MenuItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }


    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=KACPER;Initial Catalog=ragstorichesdb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;Initial Catalog=ragstorichesdb");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressesId).HasName("PK__Addresse__2495A9C434581FCB");

            entity.Property(e => e.AddressesId)
                .ValueGeneratedNever()
                .HasColumnName("AddressesID");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.ZipCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.Property(e => e.Description).IsUnicode(false);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartId);

            entity.Property(e => e.CartId).HasColumnName("CartID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SessionId).HasColumnName("SessionID");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems).HasForeignKey(d => d.ProductId);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoriesId).HasName("PK_CategoriesID");

            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.Name).HasMaxLength(25);
        });

        modelBuilder.Entity<Footer>(entity =>
        {
            entity.Property(e => e.Icon).IsUnicode(false);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("ImageID");
            entity.Property(e => e.Image1).HasColumnName("Image");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Product).WithMany(p => p.Images)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_Products");
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(e => e.IdWebsiteLinks);

            entity.Property(e => e.Link).HasMaxLength(20);
            entity.Property(e => e.LinkName).HasMaxLength(30);
            entity.Property(e => e.RightMenu)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("OrderID");
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.OrderStatusId).HasColumnName("Order_StatusID");
            entity.Property(e => e.TotalCost).HasColumnType("money");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK__Orders__Order_St__619B8048");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Users");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.HasKey(e => e.OrderProductsId).HasName("PK__Order_Pr__3C82AC26B2BB15B6");

            entity.ToTable("Order_Products");

            entity.Property(e => e.OrderProductsId)
                .ValueGeneratedNever()
                .HasColumnName("Order_ProductsID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Products_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Products_Products");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderStatusId).HasName("PK__Order_St__8EEC2EA568FFDE10");

            entity.ToTable("Order_Status");

            entity.Property(e => e.OrderStatusId)
                .ValueGeneratedNever()
                .HasColumnName("Order_StatusID");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.CategoriesId).HasColumnName("CategoriesID");
            entity.Property(e => e.Cost).HasColumnType("money");
            entity.Property(e => e.SizesId).HasColumnName("SizesID");

            entity.HasOne(d => d.Categories).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoriesId)
                .HasConstraintName("FK__Products__Catego__7F2BE32F");

            entity.HasOne(d => d.Sizes).WithMany(p => p.Products)
                .HasForeignKey(d => d.SizesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__SizesI__6477ECF3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolesId).HasName("PK__Roles__C4B27820DABFF193");

            entity.Property(e => e.RolesId).HasColumnName("RolesID");
            entity.Property(e => e.Role1)
                .HasMaxLength(25)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.SizesId).HasName("PK__Sizes__E89D5F1F74C70295");

            entity.Property(e => e.SizesId)
                .ValueGeneratedNever()
                .HasColumnName("SizesID");
            entity.Property(e => e.Size1)
                .HasMaxLength(25)
                .HasColumnName("Size");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.AddressesId).HasColumnName("AddressesID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserRolesId).HasColumnName("UserRolesID");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Addresses).WithMany(p => p.Users)
                .HasForeignKey(d => d.AddressesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Addresses__01142BA1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRolesId).HasName("PK__UserRole__43D8C0CDC0F3DDEE");

            entity.Property(e => e.UserRolesId).HasColumnName("UserRolesID");
            entity.Property(e => e.RolesId).HasColumnName("RolesID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Roles).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
