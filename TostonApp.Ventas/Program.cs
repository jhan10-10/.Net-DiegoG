    using Microsoft.EntityFrameworkCore;
using TostonApp.Ventas.Models;

var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICIOS --------------------

// MVC
builder.Services.AddControllersWithViews();

// DbContext con la cadena CORRECTA
builder.Services.AddDbContext<TostonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TostonAppConnection"))
);

var app = builder.Build();

// -------------------- MIDDLEWARE --------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();     // IMPORTANTE para CSS/JS/img

app.UseRouting();

app.UseAuthorization();

// -------------------- RUTAS --------------------

// Ruta general
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// -------------------- RUN --------------------
app.Run();
