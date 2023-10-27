
using Coursework2.Factories.Implementations;
using Coursework2.Factories.Interfaces;
using Coursework2.Interfaces;
using Coursework2.Parsers;
using Coursework2.Realizations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Coursework2.Data.Coursework2;
using Coursework2.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<CoinCapDataParser>();
builder.Services.AddScoped<CoinMarketCapDataParser>();
builder.Services.AddScoped<IApiParserFactory, ApiServiceFactory>();
builder.Services.AddScoped<ICoinCapFunctional, CoinCapService>();
builder.Services.AddScoped<ICoinMarketCapFunctional,CoinMarketCapService>();
builder.Services.AddScoped<IKeyFactory, KeyFactory>();

builder.Services.AddDbContext<DataBaseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services
    .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<DataBaseDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();




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
