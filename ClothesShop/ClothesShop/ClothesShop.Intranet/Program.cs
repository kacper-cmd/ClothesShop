using ClothesShop.Data.Data;
using ClothesShop.Intranet;
using ClothesShop.Intranet.Services.Interfaces;
using ClothesShop.Intranet.Services;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClothesShopContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesShopContext") ?? throw new InvalidOperationException("Connection string 'ClothesShopContext' not found.")));


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IBufferedFileUploadService, BufferedFileUploadLocalService>();

var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;

//    DataSeeder.Initialize(services);
//}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
IWebHostEnvironment env = app.Environment;
RotativaConfiguration.Setup(env.WebRootPath, "..\\Rotativa\\");//RotativaConfiguration.Setup(env.WebRootPath, "Rotativa"); //ROtativa in wwwroot folder


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Seed();
app.Run();
