using Coursework2.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Coursework2.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<FavouriteCurrency> FavouriteCurrencies { get; set; }
    public DbSet<RecentlyVisitedCurrency> RecentlyVisitedCurrencies { get; set; }

    public DbSet<PopularCurrencies> PopularCurrencies { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
           .HasMany(u => u.FavouriteCurrencies)
           .WithOne(f => f.User);

        builder.Entity<ApplicationUser>()
            .HasMany(u => u.RecentlyVisitedCurrencies)
            .WithOne(r => r.User);
    }
}
