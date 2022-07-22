using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Infrasructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IDbContext, BulletinBoardDbContext>(options => {
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BulletinBoardDbContext") ??
        throw new InvalidOperationException("Connection string 'BulletinBoardDbContext' not found.")
    );
});

builder.Services.AddScoped<AuthorizationAttribute>();
builder.Services.AddSingleton<IValidator, Validator>();

// Add Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(300);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BulletinBoard}/{action=Index}/{id?}");

app.Run();
