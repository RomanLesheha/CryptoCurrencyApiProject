using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coursework2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PicturePhoto { get; set; }

        public string PictureUrl { get; set;}
        public string PictureUrl2 { get; set; } 

            
    }

    public class UserData
    {
        [Key]
        public string UserId { get; set; } // This will be a foreign key to the AspNetUsers table
        public List<string> FavouriteCurrencies { get; set; }
        public List<string> LastVisitedCryptoCurrencies { get; set; }
    }
}
