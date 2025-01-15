global using ClothesShop.PortalWWW.Models.DTO;
global using ClothesShop.PortalWWW.Services.EmailService;
using ClothesShop.Data.Data;
using ClothesShop.PortalWWW.Services;
using ClothesShop.PortalWWW.Services.Common;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClothesShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesShopContext") ?? throw new InvalidOperationException("Connection string 'ClothesShopContext' not found.")));
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<CommonDataService>();
builder.Services.AddSession();

//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Index}/{id?}");

app.Run();

//https://stackoverflow.com/questions/72779826/trouble-with-seeding-data-in-asp-net-core-6
//https://github.com/techwithpat/MovieApp/blob/master/MovieApp.Data/Movie.cs