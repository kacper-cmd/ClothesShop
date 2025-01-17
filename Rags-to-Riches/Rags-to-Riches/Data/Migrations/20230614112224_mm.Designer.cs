﻿// <auto-generated />
using System;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Data.Migrations
{
    [DbContext(typeof(RagsToRichesDbContext))]
    [Migration("20230614112224_mm")]
    partial class mm
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Data.Address", b =>
                {
                    b.Property<int>("AddressesId")
                        .HasColumnType("int")
                        .HasColumnName("AddressesID");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("HomeNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ZipCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("AddressesId")
                        .HasName("PK__Addresse__2495A9C434581FCB");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Data.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ButtonText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ButtonUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("ImageNameUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("SocialIcon1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialIcon2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialIcon3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialIcon4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialLinks1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialLinks2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialLinks3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SocialLinks4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("Data.CartItem", b =>
                {
                    b.Property<int>("CartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CartID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartId"));

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<string>("SessionId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("SessionID");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Data.Category", b =>
                {
                    b.Property<int>("CategoriesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("CategoriesID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriesId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("CategoriesId")
                        .HasName("PK_CategoriesID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Data.Footer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ColumnNumber")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Footers");
                });

            modelBuilder.Entity("Data.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .HasColumnType("int")
                        .HasColumnName("ImageID");

                    b.Property<string>("Image1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Image");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Data.MenuItem", b =>
                {
                    b.Property<int>("IdWebsiteLinks")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdWebsiteLinks"));

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdditionalInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LinkName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<bool?>("RightMenu")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("(CONVERT([bit],(0)))");

                    b.HasKey("IdWebsiteLinks");

                    b.ToTable("MenuItems");
                });

            modelBuilder.Entity("Data.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("OrderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("Order_StatusID");

                    b.Property<decimal?>("TotalCost")
                        .HasColumnType("money");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Data.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductsId")
                        .HasColumnType("int")
                        .HasColumnName("Order_ProductsID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderProductsId")
                        .HasName("PK__Order_Pr__3C82AC26B2BB15B6");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order_Products", (string)null);
                });

            modelBuilder.Entity("Data.OrderStatus", b =>
                {
                    b.Property<int>("OrderStatusId")
                        .HasColumnType("int")
                        .HasColumnName("Order_StatusID");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("OrderStatusId")
                        .HasName("PK__Order_St__8EEC2EA568FFDE10");

                    b.ToTable("Order_Status", (string)null);
                });

            modelBuilder.Entity("Data.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int?>("CategoriesId")
                        .HasColumnType("int")
                        .HasColumnName("CategoriesID");

                    b.Property<decimal?>("Cost")
                        .HasColumnType("money");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("SizesId")
                        .HasColumnType("int")
                        .HasColumnName("SizesID");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoriesId");

                    b.HasIndex("SizesId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Data.Role", b =>
                {
                    b.Property<int>("RolesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RolesID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RolesId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Role1")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Role");

                    b.HasKey("RolesId")
                        .HasName("PK__Roles__C4B27820DABFF193");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Data.Size", b =>
                {
                    b.Property<int>("SizesId")
                        .HasColumnType("int")
                        .HasColumnName("SizesID");

                    b.Property<string>("Size1")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)")
                        .HasColumnName("Size");

                    b.HasKey("SizesId")
                        .HasName("PK__Sizes__E89D5F1F74C70295");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.Property<int>("AddressesId")
                        .HasColumnType("int")
                        .HasColumnName("AddressesID");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("UserRolesId")
                        .HasColumnType("int")
                        .HasColumnName("UserRolesID");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.HasIndex("AddressesId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Data.UserRole", b =>
                {
                    b.Property<int>("UserRolesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("UserRolesID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRolesId"));

                    b.Property<int>("RolesId")
                        .HasColumnType("int")
                        .HasColumnName("RolesID");

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID");

                    b.HasKey("UserRolesId")
                        .HasName("PK__UserRole__43D8C0CDC0F3DDEE");

                    b.HasIndex("RolesId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Data.CartItem", b =>
                {
                    b.HasOne("Data.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Image", b =>
                {
                    b.HasOne("Data.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Images_Products");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Order", b =>
                {
                    b.HasOne("Data.OrderStatus", "OrderStatus")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusId")
                        .HasConstraintName("FK__Orders__Order_St__619B8048");

                    b.HasOne("Data.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_Users");

                    b.Navigation("OrderStatus");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.OrderProduct", b =>
                {
                    b.HasOne("Data.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Products_Orders");

                    b.HasOne("Data.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .IsRequired()
                        .HasConstraintName("FK_Order_Products_Products");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Data.Product", b =>
                {
                    b.HasOne("Data.Category", "Categories")
                        .WithMany("Products")
                        .HasForeignKey("CategoriesId")
                        .HasConstraintName("FK__Products__Catego__7F2BE32F");

                    b.HasOne("Data.Size", "Sizes")
                        .WithMany("Products")
                        .HasForeignKey("SizesId")
                        .IsRequired()
                        .HasConstraintName("FK__Products__SizesI__6477ECF3");

                    b.Navigation("Categories");

                    b.Navigation("Sizes");
                });

            modelBuilder.Entity("Data.User", b =>
                {
                    b.HasOne("Data.Address", "Addresses")
                        .WithMany("Users")
                        .HasForeignKey("AddressesId")
                        .IsRequired()
                        .HasConstraintName("FK__Users__Addresses__01142BA1");

                    b.Navigation("Addresses");
                });

            modelBuilder.Entity("Data.UserRole", b =>
                {
                    b.HasOne("Data.Role", "Roles")
                        .WithMany("UserRoles")
                        .HasForeignKey("RolesId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Roles");

                    b.HasOne("Data.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoles_Users");

                    b.Navigation("Roles");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Data.Address", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Data.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Data.OrderStatus", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Data.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Images");

                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("Data.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Data.Size", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Data.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
