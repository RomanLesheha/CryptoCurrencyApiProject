using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Coursework2.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public List<FavouriteCurrency> FavouriteCurrencies { get; set; } = new List<FavouriteCurrency>();
    public List<RecentlyVisitedCurrency> RecentlyVisitedCurrencies { get; set; } = new List<RecentlyVisitedCurrency>();
}


public class FavouriteCurrency
{
    public int Id { get; set; }
    public string Currency { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public class RecentlyVisitedCurrency
{
    public int Id { get; set; }
    public string Currency { get; set; }

    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}

