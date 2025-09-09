using proyectopdf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using proyectopdf.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<proyectopdfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("proyectopdfContext") ?? throw new InvalidOperationException("Connection string 'proyectopdfContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<DBPRUEBASContext>();
//builder.Services.AddDbContext<proyectopdfContext>(options =>
    //options.UseSqlServer(builder.Configuration.GetConnectionString("proyectopdfContext")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Ventas}/{action=Index}/{id?}");



IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");

app.Run();
