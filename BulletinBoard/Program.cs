using Microsoft.EntityFrameworkCore;
using BulletinBoard.Models;
using BulletinBoard.Models.Repositories;
using BulletinBoard.Models.Entities;
using BulletinBoard.Models.BusinessLogic;
using BulletinBoard.Utils.Validation;
using BulletinBoard.Infrasructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<DbContext, BulletinBoardContext>();
builder.Services.AddDbContext<BulletinBoardContext>(options =>
{
    options.UseSqlite(
        builder.Configuration.GetConnectionString("BulletinBoardDbContext") ??
        throw new InvalidOperationException("Connection string 'BulletinBoardDbContext' not found.")
    );
});
builder.Services.AddScoped<AuthorizationAttribute>();
builder.Services.AddSingleton<IValidator, Validator>();
builder.Services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
builder.Services.AddScoped<IGenericRepository<Post>, GenericRepository<Post>>();
builder.Services.AddScoped<IGenericRepository<Reply>, GenericRepository<Reply>>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IRegisterLogic, RegisterLogic>();
builder.Services.AddScoped<ILoginLogic, LoginLogic>();
builder.Services.AddScoped<IBulletinBoardLogic, BulletinBoardLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<IPostLogic, PostLogic>();

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
