using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using QLSPNhom2.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Kết nối tới database
builder.Services.AddDbContext<AppDbContext>
    (options => options.UseSqlServer(@"Server=DESKTOP-BKU2AOI;Database=QLSPNhom2;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True"));
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
