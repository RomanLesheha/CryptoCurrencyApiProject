
using Coursework2.Factories.Implementations;
using Coursework2.Factories.Interfaces;
using Coursework2.Interfaces;
using Coursework2.Parsers;
using Coursework2.Realizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Coursework2.Models;
using Coursework2.Data;
using Coursework2.Areas.Identity.Data;
using Coursework2.Implementations;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CoinCapDataParser>();
builder.Services.AddScoped<CoinMarketCapDataParser>();
builder.Services.AddScoped<IApiParserFactory, ApiServiceFactory>();
builder.Services.AddScoped<ICoinCapFunctional, CoinCapService>();
builder.Services.AddScoped<ICoinMarketCapFunctional,CoinMarketCapService>();
builder.Services.AddScoped<IKeyFactory, KeyFactory>();
builder.Services.AddScoped<IUserRealization, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await DBSeeder.SeedDefaultData(scope.ServiceProvider);
}


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
